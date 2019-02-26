using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nehft.Server.Customers;

namespace Nehft.Server.Animals.AddAnimal
{
    public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAnimalRepository _animalRepository;

        public AddAnimalCommandHandler(ICustomerRepository customerRepository, IAnimalRepository animalRepository)
        {
            _customerRepository = customerRepository;
            _animalRepository = animalRepository;
        }

        public Task<Unit> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.Get(request.Customer);
            var animalId = Guid.NewGuid();
            var animal = new Animal(animalId, request.Name, request.Type);

            customer.AddAnimal(animalId);

            _customerRepository.Save(customer);
            _animalRepository.Save(animal);

            return Task.FromResult(Unit.Value);
        }
    }
}