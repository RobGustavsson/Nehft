using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Nehft.Server.Customers.AddAnimal
{
    public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddAnimalCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Unit> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.Get(request.Customer);

            customer.AddAnimal(request.Name, request.Type);

            _customerRepository.Save(customer);

            return Task.FromResult(Unit.Value);
        }
    }
}