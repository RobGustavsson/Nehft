using System;
using Nehft.Server.Horses;
using Xunit;

namespace Nehft.Server.Tests
{
    public class AggregateSpec
    {
        [Fact]
        public void Cannot_apply_event_from_another_aggregate()
        {
            var entity = new Entity(Guid.NewGuid());

            Action act = () => entity.ApplyEvent(Guid.NewGuid());

            Assert.Throws<InvalidEventException>(act);
        }

        [Fact]
        public void Events_are_handled_after_raised()
        {
            var entity = new Entity(Guid.NewGuid());

            Assert.True(entity.HasHandledEvent);
        }
    }

    public class Entity : Aggregate<Entity>
    {
        public bool HasHandledEvent { get; private set; }
        public Entity(Guid id)
        {
            Id = id;
            RaiseEvent(new EntityEvent(id));
        }

        public void Handle(EntityEvent @event)
        {
            HasHandledEvent = true;
        }

        public void ApplyEvent(Guid id)
        {
            RaiseEvent(new EntityEvent(id));
        }
    }

    public class EntityEvent : AggregateEvent<Entity>
    {
        public EntityEvent(Guid id) : base(id)
        {
        }

        public override void Accept(Entity aggregate)
        {
            aggregate.Handle(this);
        }
    }
}
