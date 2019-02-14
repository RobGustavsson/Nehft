using System;
using System.Collections.Generic;

namespace Nehft.Server.Customers
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
        Customer Get(Guid customerId);
        IEnumerable<Customer> GetAll();
    }

    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private Dictionary<Guid, IEnumerable<IAggregateEvent>> _database
            = new Dictionary<Guid, IEnumerable<IAggregateEvent>>();

        public void Save(Customer customer)
        {
            _database.Add(customer.Id, customer.Events);
        }

        public Customer Get(Guid customerId)
        {
            var events = _database[customerId];

            return new Customer(events);
        }

        public IEnumerable<Customer> GetAll()
        {
            foreach (var customerId in _database.Keys)
            {
                var events = _database[customerId];
                yield return new Customer(events);
            }
        }

        public void Clear()
        {
            _database = new Dictionary<Guid, IEnumerable<IAggregateEvent>>();
        }
    }
}