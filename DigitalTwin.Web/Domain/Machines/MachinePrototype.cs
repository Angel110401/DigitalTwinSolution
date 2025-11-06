using DigitalTwin.Web.Domain.Events;
using DigitalTwin.Web.Domain.Machines.State;
using DigitalTwin.Web.Domain.Sensors;

namespace DigitalTwin.Web.Domain.Machines
{
    public class MachinePrototype
    {
        public string Name { get; init; } = "Machine";
        public (double t, double p, double rpm) Initial { get; init; } = (25, 1.0, 0);

        public Machine Clone(IEventBus bus) => new(
            Name,
            bus,
            new BasicSensor("Temperature", Initial.t),
            new BasicSensor("Pressure", Initial.p),
            new BasicSensor("RPM", Initial.rpm),
            new OffState());
    }
}

