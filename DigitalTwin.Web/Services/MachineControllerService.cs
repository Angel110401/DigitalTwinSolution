using DigitalTwin.Web.Domain.Machines;

namespace DigitalTwin.Web.Services
{
    public interface ICommand { Task ExecuteAsync(CancellationToken ct); }

    public record StartMachineCommand(Machine M) : ICommand
    { public Task ExecuteAsync(CancellationToken ct) => M.StartAsync(ct); }

    public record StopMachineCommand(Machine M) : ICommand
    { public Task ExecuteAsync(CancellationToken ct) => M.StopAsync(ct); }

    public record SetMaintenanceCommand(Machine M) : ICommand
    { public Task ExecuteAsync(CancellationToken ct) => M.SetMaintenanceAsync(ct); }

    public record AcknowledgeErrorCommand(Machine M) : ICommand
    { public Task ExecuteAsync(CancellationToken ct) => M.AcknowledgeErrorAsync(ct); }

    public class MachineControllerService
    {
        public Task RunAsync(ICommand cmd, CancellationToken ct) => cmd.ExecuteAsync(ct);
    }
}
