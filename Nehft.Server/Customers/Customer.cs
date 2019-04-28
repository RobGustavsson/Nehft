using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Nehft.Server.Customers
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public partial class Customer : Aggregate<Customer>
    {
        private readonly List<Guid> _horses = new List<Guid>();
        public Name Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Address Address { get; private set; }

        public IEnumerable<Guid> Horses => _horses;

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

        public void AddHorse(Guid horseId)
        {
            RaiseEvent(new AddHorseEvent(Id, horseId));
        }
    }
}