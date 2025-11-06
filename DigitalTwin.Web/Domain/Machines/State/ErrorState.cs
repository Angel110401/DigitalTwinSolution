namespace DigitalTwin.Web.Domain.Machines.State
{
    public class ErrorState : IMachineState
    {
        public string Name => "Error";
        public Task EnterAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task ExitAsync(Machine c, CancellationToken ct) => Task.CompletedTask;

        // Para salir del error, usamos Ack → vuelve a Off
        public Task AcknowledgeErrorAsync(Machine c, CancellationToken ct) => c.TransitionAsync(new OffState(), ct);

        public Task SetMaintenanceAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task StartAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
        public Task StopAsync(Machine c, CancellationToken ct) => Task.CompletedTask;
    }
}

