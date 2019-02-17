using System;
using System.Collections.Generic;

namespace Nehft.Server.Customers
{
    public class Customer : Aggregate
    {
        private readonly List<Animal> _animals = new List<Animal>();
        public Name Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Address Address { get; private set; }

        public IEnumerable<Animal> Animals => _animals;

        public Customer(IEnumerable<IAggregateEvent> events)
        {
            Rehydrate(events);
        }

        public Customer(Guid id, Name name, EmailAddress email, Address address)
        {
            Id = id;
            RaiseEvent(new CreateCustomerEvent(id, name, email, address));
        }
        
        protected override void ApplyEvent(IAggregateEvent @event)
        {
            switch (@event)
            {
                case CreateCustomerEvent createEvent:
                    Id = createEvent.Id; 
                    Name = createEvent.Name;
                    Email = createEvent.Email;
                    Address = createEvent.Address;
                    break;
                case AddAnimalEvent addAnimalEvent:
                    _animals.Add(new Animal(addAnimalEvent.Name, addAnimalEvent.Type));
                    break;
            }
        }

        public void AddAnimal(string animalName, string animalType)
        {
            RaiseEvent(new AddAnimalEvent(Id, animalName, animalType));
        }
    }

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
