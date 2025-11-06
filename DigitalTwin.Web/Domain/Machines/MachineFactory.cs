using DigitalTwin.Web.Domain.Events;

namespace DigitalTwin.Web.Domain.Machines
{
    public class MachineFactory
    {
        public Machine Create(string type, IEventBus bus)
            => type switch
            {
                "Press" => new MachinePrototype { Name = "Press", Initial = (28, 3.2, 0) }.Clone(bus),
                "Conveyor" => new MachinePrototype { Name = "Conveyor", Initial = (26, 1.1, 200) }.Clone(bus),
                _ => new MachinePrototype { Name = type }.Clone(bus)
            };
    }
}
