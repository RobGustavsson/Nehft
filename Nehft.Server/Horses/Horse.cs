using System;
using System.Collections.Generic;
using System.Linq;

namespace Nehft.Server.Horses
{
    public partial class Horse : Aggregate<Horse>
    {
        public string Name { get; private set; }
        public HorseType Type { get; private set; }
        public string Breed { get; private set; }
        public string Exterior { get; private set; }
        public string History { get; private set; }
        public int YearOfBirth { get; private set; }
        public Address Address { get; private set; }

        public Horse(IReadOnlyList<IAggregateEvent> events)
        {
            Id = events.First().EntityId;
            Rehydrate(events);
        }

        public Horse(Guid id, string name, HorseType type, string breed, string exterior, string history, int yearOfBirth, Address address)
        {
            Id = id;
            RaiseEvent(new CreateHorseEvent(id, name, type, breed, exterior, history, yearOfBirth, address));
        }
    }
}