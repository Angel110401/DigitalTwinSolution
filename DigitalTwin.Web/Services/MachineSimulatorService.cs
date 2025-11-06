using DigitalTwin.Web.Domain.Events;
using DigitalTwin.Web.Domain.Machines;
using DigitalTwin.Web.Domain.Machines.State;
using DigitalTwin.Web.Domain.Strategy;
using DigitalTwin.Web.Infrastructure;

namespace DigitalTwin.Web.Services
{
    public class MachineSimulatorService : BackgroundService
    {
        private readonly IMachineRepository _repo;
        private readonly IEventBus _bus;
        private readonly IProjectionStrategy _strategy = new RandomWalkProjection();

        public MachineSimulatorService(IMachineRepository repo, IEventBus bus)
        { _repo = repo; _bus = bus; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_repo.All().Any())
            {
                var factory = new MachineFactory();
                _repo.Add(factory.Create("Press", _bus));
                _repo.Add(factory.Create("Conveyor", _bus));
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var m in _repo.All())
                {
                    if (m.State is OperationalState && Random.Shared.NextDouble() < 0.01)
                    {
                        var next = _strategy.Next((m.Temperature.Value, m.Pressure.Value, m.Rpm.Value));
                        m.Temperature.Update(next.t);
                        m.Pressure.Update(next.p);
                        m.Rpm.Update(next.rpm);
                        await m.TransitionAsync(new ErrorState(), stoppingToken);
                        await _bus.PublishAsync(MachineEvent.Error(m.Id, m.Name, "Random fault injected"));
                        await _bus.PublishAsync(MachineEvent.Telemetry(m.Id, m.Name, next.t, next.p, next.rpm));
                    }
                }

                await Task.Delay(250, stoppingToken);
            }
        }
    }
}

