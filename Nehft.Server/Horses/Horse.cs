using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server.Horses
{
    public class Horse : Aggregate<Horse>
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Horse(Guid id, string name, string type)
        {
            Id = id;
            RaiseEvent(new CreateHorseEvent(id, name, type));
        }

        public Horse(IReadOnlyList<IAggregateEvent> events)
        {
            Id = events.First().EntityId;
            Rehydrate(events);
        }

        private void Handle(CreateHorseEvent @event)
        {
            Name = @event.Name;
            Type = @event.Type;
        }
    }

    public class CreateHorseEvent : IAggregateEvent
    {
        public CreateHorseEvent(Guid entityId, string name, string type)
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