using System.Collections.Generic;

namespace Nehft.Server.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }

    public class InMemoryCustomerRepository : InMemoryRepositoryBase<Customer>, ICustomerRepository
    {
        public override Customer CreateAggregate(IReadOnlyList<IAggregateEvent> events)
        {
            return new Customer(events);
        }
    }
}