using System.Reflection.PortableExecutable;

namespace DigitalTwin.Web.Domain.Machines.State
{
    public interface IMachineState
    {
        string Name { get; }
        Task EnterAsync(Machine context, CancellationToken ct);
        Task ExitAsync(Machine context, CancellationToken ct);
        Task StartAsync(Machine context, CancellationToken ct);
        Task StopAsync(Machine context, CancellationToken ct);
        Task SetMaintenanceAsync(Machine context, CancellationToken ct);
        Task AcknowledgeErrorAsync(Machine context, CancellationToken ct);
    }
}

