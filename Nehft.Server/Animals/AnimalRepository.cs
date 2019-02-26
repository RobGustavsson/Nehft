using System.Collections.Generic;

namespace Nehft.Server.Animals
{
    public interface IAnimalRepository : IRepository<Animal>
    {

    }

    public class InMemoryAnimalRepository : InMemoryRepositoryBase<Animal>, IAnimalRepository
    {
        public override Animal CreateAggregate(IReadOnlyList<IAggregateEvent> events)
        {
            return new Animal(events);
        }
    }
}