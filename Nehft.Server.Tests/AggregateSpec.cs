using System;
using Xunit;

namespace Nehft.Server.Tests
{
    public class Entity : Aggregate<Entity>
    {
        public Entity(Guid id)
        {
            Id = id;
            RaiseEvent(new EntityEvent(id));
        }

        public void ApplyEvent(Guid id)
        {
            RaiseEvent(new EntityEvent(id));
        }
    }

    public class EntityEvent : IAggregateEvent
    {
        public EntityEvent(Guid id)
        {
            EntityId = id;
        }

        public Guid EntityId { get; }
    }
    public class AggregateSpec
    {
        [Fact]
        public void Cannot_apply_event_from_another_aggregate()
        {
            var entity = new Entity(Guid.NewGuid());

            Assert.Throws<InvalidEventException>(() => entity.ApplyEvent(Guid.NewGuid()));
        }
    }
}
