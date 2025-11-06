using System.Collections.Concurrent;
using DigitalTwin.Web.Domain.Machines;

namespace DigitalTwin.Web.Infrastructure
{
    public interface IMachineRepository
    {
        IEnumerable<Machine> All();
        Machine? Get(Guid id);
        void Add(Machine m);
    }

    public class InMemoryMachineRepository : IMachineRepository
    {
        private readonly ConcurrentDictionary<Guid, Machine> _store = new();
        public IEnumerable<Machine> All() => _store.Values.ToArray();
        public Machine? Get(Guid id) => _store.TryGetValue(id, out var m) ? m : null;
        public void Add(Machine m) => _store[m.Id] = m;
    }
}
