namespace DigitalTwin.Web.Domain.Sensors
{
    public interface ISensor
    {
        string Name { get; }
        double Value { get; }
        void Update(double next);
    }

    public class BasicSensor : ISensor
    {
        public string Name { get; }
        public double Value { get; private set; }
        public BasicSensor(string name, double initial) { Name = name; Value = initial; }
        public void Update(double next) => Value = next;
    }
}
