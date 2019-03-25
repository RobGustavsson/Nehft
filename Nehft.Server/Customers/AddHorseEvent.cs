using System;

namespace Nehft.Server.Customers
{
    public class AddHorseEvent : IAggregateEvent
    {
        public Guid EntityId { get; }
        public Guid HorseId { get; }

        public AddHorseEvent(Guid id, Guid horseId)
        {
            EntityId = id;
            HorseId = horseId;
        }
    }
}