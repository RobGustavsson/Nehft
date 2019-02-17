using System;

namespace Nehft.Server.Customers
{
    public class AddAnimalEvent : IAggregateEvent
    {
        public string Name { get; }
        public string Type { get; }
        public Guid Id { get; }

        public AddAnimalEvent(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}