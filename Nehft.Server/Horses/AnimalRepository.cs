using System.Collections.Generic;

namespace Nehft.Server.Horses
{
    public interface IHorseRepository : IRepository<Horse>
    {

    }

    public class InMemoryHorseRepository : InMemoryRepositoryBase<Horse>, IHorseRepository
    {
        public override Horse CreateAggregate(IReadOnlyList<IAggregateEvent> events)
        {
            return new Horse(events);
        }
    }
}