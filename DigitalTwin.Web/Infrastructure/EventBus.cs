using System.Collections.Concurrent;
using DigitalTwin.Web.Domain.Events;

namespace DigitalTwin.Web.Infrastructure
{
    public class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<string, List<Func<MachineEvent, Task>>> _subs = new();
        private readonly ConcurrentQueue<MachineEvent> _recent = new();

        public void Subscribe(string topic, Func<MachineEvent, Task> handler)
        {
            _subs.AddOrUpdate(topic, _ => new() { handler }, (_, list) => { list.Add(handler); return list; });
        }

        public Task PublishAsync(MachineEvent evt)
        {
            _recent.Enqueue(evt);
            while (_recent.Count > 500) _recent.TryDequeue(out _);
            if (_subs.TryGetValue(evt.Type, out var handlers))
                return Task.WhenAll(handlers.Select(h => h(evt)));
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<MachineEvent> Recent(int take = 200)
            => _recent.Reverse().Take(take).ToArray();
    }
}

