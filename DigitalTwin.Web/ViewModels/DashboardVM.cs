namespace DigitalTwin.Web.ViewModels
{
    public class MachineVM
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public double Temperature { get; init; }
        public double Pressure { get; init; }
        public double Rpm { get; init; }
    }

    public class DashboardVM
    {
        public List<MachineVM> Machines { get; } = new();
        public IEnumerable<string> RecentEvents { get; set; } = Enumerable.Empty<string>();
    }
}
