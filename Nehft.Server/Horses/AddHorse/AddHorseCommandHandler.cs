using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nehft.Server.Customers;

namespace Nehft.Server.Horses.AddHorse
{
    public class AddHorseCommandHandler : IRequestHandler<AddHorseCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHorseRepository _horseRepository;

        public AddHorseCommandHandler(ICustomerRepository customerRepository, IHorseRepository horseRepository)
        {
            _customerRepository = customerRepository;
            _horseRepository = horseRepository;
        }

        public Task<Unit> Handle(AddHorseCommand request, CancellationToken cancellationToken)
        {
            var customer = _customerRepository.Get(request.Customer);
            var animalId = Guid.NewGuid();
            var animal = new Horse(animalId, request.Name, request.Type, request.Breed, request.Exterior, request.History, request.YearOfBirth, request.Address);

            customer.AddHorse(animalId);

            _customerRepository.Save(customer);
            _horseRepository.Save(animal);

            return Task.FromResult(Unit.Value);
        }
    }
}