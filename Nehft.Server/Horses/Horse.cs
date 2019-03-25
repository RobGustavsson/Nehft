using System;
using System.Collections.Generic;
using System.Linq;
using Nehft.Server.Horses.AddHorse;

namespace Nehft.Server.Horses
{
    public class Horse : Aggregate<Horse>
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

        private void Handle(CreateHorseEvent @event)
        {
            Name = @event.Name;
            Type = @event.Type;
            Breed = @event.Breed;
            Exterior = @event.Exterior;
            History = @event.History;
            YearOfBirth = @event.YearOfBirth;
            Address = @event.Address;
        }
    }

    public class CreateHorseEvent : IAggregateEvent
    {
        public CreateHorseEvent(Guid id, string name, HorseType type, string breed, string exterior, string history, int yearOfBirth, Address address)
        {
            EntityId = id;
            Name = name;
            Type = type;
            Breed = breed;
            Exterior = exterior;
            History = history;
            YearOfBirth = yearOfBirth;
            Address = address;
        }

        public Guid EntityId { get; }
        public string Name { get; }
        public HorseType Type { get; }
        public string Breed { get; }
        public string Exterior { get; }
        public string History { get; }
        public int YearOfBirth { get; }
        public Address Address { get; }
    }
}