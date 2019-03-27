using System;

namespace Nehft.Server.Horses
{
    public partial class Horse
    {
        private class CreateHorseEvent : AggregateEvent<Horse>
        {
            public CreateHorseEvent(Guid id, string name, HorseType type, string breed, string exterior, string history, int yearOfBirth, Address address) : base(id)
            {
                Name = name;
                Type = type;
                Breed = breed;
                Exterior = exterior;
                History = history;
                YearOfBirth = yearOfBirth;
                Address = address;
            }

            public string Name { get; }
            public HorseType Type { get; }
            public string Breed { get; }
            public string Exterior { get; }
            public string History { get; }
            public int YearOfBirth { get; }
            public Address Address { get; }

            public override void Accept(Horse aggregate)
            {
                aggregate.Handle(this);
            }
        }
    }
}