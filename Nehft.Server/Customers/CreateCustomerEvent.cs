using System;

namespace Nehft.Server.Customers
{
    public class CreateCustomerEvent : IAggregateEvent
    {
        public Name Name { get; }
        public EmailAddress Email { get; }
        public Address Address { get; }
        public Guid Id { get; }

        public CreateCustomerEvent(Guid id, Name name, EmailAddress email, Address address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }

        public void Accept(Aggregate aggregate)
        {
            var customer = (Customer)aggregate;
            customer.Handle(this);
        }
    }
}