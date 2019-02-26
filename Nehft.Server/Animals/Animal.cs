using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server.Animals
{
    public class Animal : Aggregate<Animal>
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Animal(Guid id, string name, string type)
        {
            Id = id;
            RaiseEvent(new CreateAnimalEvent(id, name, type));
        }

        public Animal(IReadOnlyList<IAggregateEvent> events)
        {
            Id = events.First().EntityId;
            Rehydrate(events);
        }

        public void Handle(CreateAnimalEvent @event)
        {
            Name = @event.Name;
            Type = @event.Type;
        }
    }

    public class CreateAnimalEvent : IAggregateEvent
    {
        public CreateAnimalEvent(Guid entityId, string name, string type)
        {
            EntityId = entityId;
            Name = name;
            Type = type;
        }

        public Guid EntityId { get; }
        public string Name { get; }
        public string Type { get; }
    }
}