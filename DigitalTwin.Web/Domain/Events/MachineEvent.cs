namespace DigitalTwin.Web.Domain.Events
{
    public record MachineEvent(DateTime Timestamp, Guid MachineId, string MachineName, string Type, string Payload)
    {
        public static MachineEvent StateChanged(Guid id, string name, string state)
            => new(DateTime.UtcNow, id, name, "StateChanged", state);

        public static MachineEvent Telemetry(Guid id, string name, double t, double p, double rpm)
            => new(DateTime.UtcNow, id, name, "Telemetry", $"T={t:F1},P={p:F1},RPM={rpm:F0}");

        public static MachineEvent Error(Guid id, string name, string msg)
            => new(DateTime.UtcNow, id, name, "Error", msg);
    }

    public interface IEventBus
    {
        void Subscribe(string topic, Func<MachineEvent, Task> handler);
        Task PublishAsync(MachineEvent evt);
        IReadOnlyCollection<MachineEvent> Recent(int take = 200);
    }
}

