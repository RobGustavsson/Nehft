using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Nehft.Server.Customers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public Task<GetCustomerResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.Get(request.CustomerId);
            return Task.FromResult(_mapper.Map<GetCustomerResult>(customer));
        }
    }
}