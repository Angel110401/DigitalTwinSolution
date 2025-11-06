using DigitalTwin.Web.Infrastructure;
using DigitalTwin.Web.Domain.Events;
using DigitalTwin.Web.Services;
using DigitalTwin.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTwin.Web.Controllers
{
    public class MachinesController : Controller
    {
        private readonly IMachineRepository _repo;
        private readonly IEventBus _bus;
        private readonly MachineControllerService _ctrl;

        public MachinesController(IMachineRepository repo, IEventBus bus, MachineControllerService ctrl)
        { _repo = repo; _bus = bus; _ctrl = ctrl; }

        public IActionResult Index()
        {
            var vm = new DashboardVM();
            foreach (var m in _repo.All())
            {
                vm.Machines.Add(new MachineVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    State = m.State.Name,
                    Temperature = m.Temperature.Value,
                    Pressure = m.Pressure.Value,
                    Rpm = m.Rpm.Value
                });
            }
            vm.RecentEvents = _bus.Recent(20).Select(e => $"{e.Timestamp:HH:mm:ss} [{e.MachineName}] {e.Type}: {e.Payload}");
            return View(vm);
        }

        [HttpPost] public async Task<IActionResult> Start(Guid id, CancellationToken ct) { var m = _repo.Get(id); if (m is null) return NotFound(); await _ctrl.RunAsync(new StartMachineCommand(m), ct); return RedirectToAction(nameof(Index)); }
        [HttpPost] public async Task<IActionResult> Stop(Guid id, CancellationToken ct) { var m = _repo.Get(id); if (m is null) return NotFound(); await _ctrl.RunAsync(new StopMachineCommand(m), ct); return RedirectToAction(nameof(Index)); }
        [HttpPost] public async Task<IActionResult> Maintenance(Guid id, CancellationToken ct) { var m = _repo.Get(id); if (m is null) return NotFound(); await _ctrl.RunAsync(new SetMaintenanceCommand(m), ct); return RedirectToAction(nameof(Index)); }
        [HttpPost] public async Task<IActionResult> Ack(Guid id, CancellationToken ct) { var m = _repo.Get(id); if (m is null) return NotFound(); await _ctrl.RunAsync(new AcknowledgeErrorCommand(m), ct); return RedirectToAction(nameof(Index)); }
    }
}
