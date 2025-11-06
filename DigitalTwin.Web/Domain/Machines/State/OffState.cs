using System.Net.NetworkInformation;

namespace DigitalTwin.Web.Domain.Machines.State
{
    public class OffState : IMachineState
    {
        public string Name => "Off";
        public Task EnterAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task ExitAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task AcknowledgeErrorAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task SetMaintenanceAsync(Machine c, CancellationToken ct) => c.TransitionAsync(new MaintenanceState(), ct);
        public Task StartAsync(Machine c, CancellationToken ct) => c.TransitionAsync(new OperationalState(), ct);
        public Task StopAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
    }
}

