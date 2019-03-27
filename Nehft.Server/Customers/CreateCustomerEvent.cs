using System;
using Nehft.Server.Horses;

namespace Nehft.Server.Customers
{
    public class CreateCustomerEvent : AggregateEvent<Customer>
    {
        public Name Name { get; }
        public EmailAddress Email { get; }
        public Address Address { get; }

        public CreateCustomerEvent(Guid id, Name name, EmailAddress email, Address address) : base(id)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        public override void Accept(Customer aggregate)
        {
            aggregate.Handle(this);
        }
    }
}