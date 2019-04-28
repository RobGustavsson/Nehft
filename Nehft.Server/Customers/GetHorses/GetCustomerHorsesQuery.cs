using MediatR;
using System;
using System.Collections.Generic;

namespace Nehft.Server.Customers.GetHorses
{
    public class GetCustomerHorsesQuery : IRequest<IEnumerable<GetCustomerHorsesResult>>
    {
        public GetCustomerHorsesQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
