using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Nehft.Server.Customers
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Customer : Aggregate<Customer>
    {
        private readonly List<Guid> _animals = new List<Guid>();
        public Name Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Address Address { get; private set; }

        public IEnumerable<Guid> Animals => _animals;

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

        public void AddAnimal(Guid animalId)
        {
            RaiseEvent(new AddAnimalEvent(Id, animalId));
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
            _animals.Add(@event.AnimalId);
        }
    }
}