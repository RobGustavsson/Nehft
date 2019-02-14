using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Nehft.Server.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid, string>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Result<Guid, string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                Name.Create(request.FirstName, request.LastName)
                    .OnSuccess(name => EmailAddress.Create(request.Email)
                        .OnSuccess(email => Address.Create(request.Street, request.StreetNumber, request.Town, request.Zipcode)
                            .OnSuccess(address =>
                            {
                                var customerId = Guid.NewGuid();
                                _customerRepository.Save(new Customer(customerId, name, email, address));
                                return customerId;
                            }))));
        }
    }
}