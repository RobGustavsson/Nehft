using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server
{
    public abstract class Aggregate<T> where T : Aggregate<T>
    {
        private readonly Queue<AggregateEvent<T>> _currentEvents = new Queue<AggregateEvent<T>>();
        public Guid Id { get; set; }

        protected void RaiseEvent(AggregateEvent<T> @event)
        {
            if (@event.EntityId != Id)
            {
                throw new InvalidEventException();
            }

            @event.Update((T)this);

            _currentEvents.Enqueue(@event);
        }

        protected void Rehydrate(IReadOnlyList<IAggregateEvent> events)
        {
            foreach (var @event in events)
            {
                RaiseEvent(@event as AggregateEvent<T>);
            }
        }

        public IReadOnlyCollection<IAggregateEvent> Events => _currentEvents.ToList();
    }

    public class InvalidEventException : Exception
    {
    }
}