using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server
{
    public interface IRepository<T> where T : Aggregate<T>
    {
        void Save(T aggregate);
        T Get(Guid aggregateId);
        IEnumerable<T> GetAll();
    }

    public abstract class InMemoryRepositoryBase<T> : IRepository<T> where T : Aggregate<T>
    {
        private Dictionary<Guid, List<IAggregateEvent>> _database
            = new Dictionary<Guid, List<IAggregateEvent>>();

        public void Save(T aggregate)
        {
            if (_database.ContainsKey(aggregate.Id))
            {
                _database[aggregate.Id].AddRange(aggregate.Events);
            }
            else
            {
                _database.Add(aggregate.Id, aggregate.Events.ToList());
            }
        }

        public T Get(Guid aggregateId)
        {
            var events = _database[aggregateId];

            return CreateAggregate(events);
        }

        public IEnumerable<T> GetAll()
        {
            foreach (var customerId in _database.Keys)
            {
                var events = _database[customerId];
                yield return CreateAggregate(events);
            }
        }

        public abstract T CreateAggregate(IReadOnlyList<IAggregateEvent> events);

        public void Clear()
        {
            _database = new Dictionary<Guid, List<IAggregateEvent>>();
        }
    }
}