using System;

namespace Nehft.Server.Customers
{
    public class AddAnimalEvent : IAggregateEvent
    {
        public string Name { get; }
        public string Type { get; }
        public Guid EntityId { get; }

        public AddAnimalEvent(Guid id, string name, string type)
        {
            EntityId = id;
            Name = name;
            Type = type;
        }
    }
}