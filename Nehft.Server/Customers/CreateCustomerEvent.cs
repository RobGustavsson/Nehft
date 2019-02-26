using System;

namespace Nehft.Server.Customers
{
    public class CreateCustomerEvent : IAggregateEvent
    {
        public Name Name { get; }
        public EmailAddress Email { get; }
        public Address Address { get; }
        public Guid EntityId { get; }

        public CreateCustomerEvent(Guid id, Name name, EmailAddress email, Address address)
        {
            EntityId = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}