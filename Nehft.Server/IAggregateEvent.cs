using System;

namespace Nehft.Server
{
    public interface IAggregateEvent
    {
        Guid Id { get; }

        void Accept(Aggregate aggregate);
    }
}