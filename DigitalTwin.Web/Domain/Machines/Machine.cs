using DigitalTwin.Web.Domain.Events;
using DigitalTwin.Web.Domain.Machines.State;
using DigitalTwin.Web.Domain.Sensors;

namespace DigitalTwin.Web.Domain.Machines
{
    public class Machine
    {
        private readonly IEventBus _bus;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public IMachineState State { get; private set; }

        public ISensor Temperature { get; }
        public ISensor Pressure { get; }
        public ISensor Rpm { get; }

        public Machine(string name, IEventBus bus, ISensor temp, ISensor pressure, ISensor rpm, IMachineState initial)
        {
            Name = name; _bus = bus;
            Temperature = temp; Pressure = pressure; Rpm = rpm;
            State = initial;
        }

        public async Task TransitionAsync(IMachineState next, CancellationToken ct)
        {
            await _lock.WaitAsync(ct);
            try
            {
                await State.ExitAsync(this, ct);
                State = next;
                await _bus.PublishAsync(MachineEvent.StateChanged(Id, Name, State.Name));
                await State.EnterAsync(this, ct);
            }
            finally { _lock.Release(); }
        }

        public Task StartAsync(CancellationToken ct) => State.StartAsync(this, ct);
        public Task StopAsync(CancellationToken ct) => State.StopAsync(this, ct);
        public Task SetMaintenanceAsync(CancellationToken ct) => State.SetMaintenanceAsync(this, ct);
        public Task AcknowledgeErrorAsync(CancellationToken ct) => State.AcknowledgeErrorAsync(this, ct);
    }
}

