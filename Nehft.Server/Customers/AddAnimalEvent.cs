using System;

namespace Nehft.Server.Customers
{
    public class AddAnimalEvent : IAggregateEvent
    {
        public Guid EntityId { get; }
        public Guid AnimalId { get; }

        public AddAnimalEvent(Guid id, Guid animalId)
        {
            EntityId = id;
            AnimalId = animalId;
        }
    }
}