using System;
using System.Collections.Generic;

namespace Nehft.Server.Customers
{
    public class Customer : Aggregate
    {     
        public Name Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public Address Address { get; private set; }

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
                    Name = createEvent.Name;
                    Email = createEvent.Email;
                    Address = createEvent.Address;
                    break;
            }
        }

    }
}
