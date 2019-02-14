using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server
{
    public abstract class Aggregate
    {
        private readonly Queue<IAggregateEvent> _currentEvents = new Queue<IAggregateEvent>();
        public Guid Id { get; protected set; }

        protected void RaiseEvent(IAggregateEvent @event)
        {
            ApplyEvent(@event);
            _currentEvents.Enqueue(@event);
        }

        protected void Rehydrate(IEnumerable<IAggregateEvent> events)
        {
            foreach (var @event in events)
            {
                RaiseEvent(@event);
            }
        }

        protected abstract void ApplyEvent(IAggregateEvent @event);

        public IReadOnlyCollection<IAggregateEvent> Events => _currentEvents.ToList();

    }
}