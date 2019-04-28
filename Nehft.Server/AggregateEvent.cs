using System;

namespace Nehft.Server
{
    public abstract class AggregateEvent<TEntity> : IAggregateEvent where TEntity : Aggregate<TEntity>
    {
        protected AggregateEvent(Guid entityId)
        {
            EntityId = entityId;
        }
        public Guid EntityId { get; }

        public abstract void Update(TEntity aggregate);
    }
}