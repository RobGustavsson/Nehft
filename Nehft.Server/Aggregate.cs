using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server
{
    public abstract class Aggregate<T> where T : Aggregate<T>
    {
        private readonly Queue<IAggregateEvent> _currentEvents = new Queue<IAggregateEvent>();
        public Guid Id { get; protected set; }

        protected void RaiseEvent(IAggregateEvent @event)
        {
            if (@event.EntityId != Id)
            {
                throw new InvalidEventException();
            }

            RedirectToHandle.InvokeEvent((T)this, @event);
            _currentEvents.Enqueue(@event);
        }

        protected void Rehydrate(IEnumerable<IAggregateEvent> events)
        {
            foreach (var @event in events)
            {
                RaiseEvent(@event);
            }
        }

        public IReadOnlyCollection<IAggregateEvent> Events => _currentEvents.ToList();

    }

    public class InvalidEventException : Exception
    {
    }
}