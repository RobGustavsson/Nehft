using System;

namespace Nehft.Server.Customers
{
    public partial class Customer
    {
        private class AddHorseEvent : AggregateEvent<Customer>
        {
            public Guid HorseId { get; }

            public AddHorseEvent(Guid id, Guid horseId) : base(id)
            {
                HorseId = horseId;
            }

            public override void Visit(Customer aggregate)
            {
                aggregate._horses.Add(HorseId);
            }
        }
        
        private class CreateCustomerEvent : AggregateEvent<Customer>
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

            public override void Visit(Customer aggregate)
            {
                aggregate.Id = EntityId;
                aggregate.Name = Name;
                aggregate.Email = Email;
                aggregate.Address = Address;
            }
        }
    }
}