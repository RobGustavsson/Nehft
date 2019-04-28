using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nehft.Server.Horses;

namespace Nehft.Server.Customers.GetHorses
{
    public class GetCustomerHorsesQueryHandler : IRequestHandler<GetCustomerHorsesQuery, IEnumerable<GetCustomerHorsesResult>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHorseRepository _horseRepository;

        public GetCustomerHorsesQueryHandler(ICustomerRepository customerRepository, IHorseRepository horseRepository)
        {
            _customerRepository = customerRepository;
            _horseRepository = horseRepository;
        }
        public Task<IEnumerable<GetCustomerHorsesResult>> Handle(GetCustomerHorsesQuery request, CancellationToken cancellationToken)
        {
            //TODO: Handle non existing customers
            var customer = _customerRepository.Get(request.CustomerId);

            var customerHorses = _horseRepository
                .GetAll()
                .Where(x => customer.Horses.Contains(x.Id))
                .Select(x => new GetCustomerHorsesResult(x.Id, x.Name, x.Type));

            return Task.FromResult(customerHorses);

        }
    }
}
