using System;
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

        [Fact]
        public void Throws_if_event_is_not_handled()
        {
            var entity = new Entity(Guid.NewGuid());

            Action act = () => entity.ApplyUnHandledEvent();

            Assert.Throws<EventNotHandledException>(act);
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

        public void ApplyUnHandledEvent()
        {
            RaiseEvent(new UnhandledEvent(Id));
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

    public class UnhandledEvent : IAggregateEvent
    {
        public UnhandledEvent(Guid id)
        {
            EntityId = id;
        }

        public Guid EntityId { get; }
    }
}
