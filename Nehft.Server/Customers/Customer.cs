using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Nehft.Server.Customers
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Customer : Aggregate<Customer>
    {
        private readonly List<Animal> _animals = new List<Animal>();
        public Name Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Address Address { get; private set; }

        public IEnumerable<Animal> Animals => _animals;

        public Customer(IReadOnlyList<IAggregateEvent> events)
        {
            Id = events.First().EntityId;
            Rehydrate(events);
        }

        public Customer(Guid id, Name name, EmailAddress email, Address address)
        {
            Id = id;
            RaiseEvent(new CreateCustomerEvent(id, name, email, address));
        }

        public void AddAnimal(string animalName, string animalType)
        {
            RaiseEvent(new AddAnimalEvent(Id, animalName, animalType));
        }

        private void Handle(CreateCustomerEvent @event)
        {
            Id = @event.EntityId;
            Name = @event.Name;
            Email = @event.Email;
            Address = @event.Address;
        }

        private void Handle(AddAnimalEvent @event)
        {
            _animals.Add(new Animal(@event.Name, @event.Type));
        }
    }
}