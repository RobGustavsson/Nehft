using System;
using MediatR;

namespace Nehft.Server.Customers.GetCustomer
{
    public class GetCustomerQuery : IRequest<GetCustomerResult>
    {
        public Guid CustomerId { get; }

        public GetCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}