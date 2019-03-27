using System;
using Nehft.Server.Horses;

namespace Nehft.Server.Customers
{
    public class AddHorseEvent : AggregateEvent<Customer>
    {
        public Guid HorseId { get; }

        public AddHorseEvent(Guid id, Guid horseId) : base(id)
        {
            HorseId = horseId;
        }

        public override void Accept(Customer aggregate)
        {
            aggregate.Handle(this);
        }
    }
}