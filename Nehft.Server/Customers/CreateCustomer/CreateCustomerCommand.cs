using System;
using MediatR;

namespace Nehft.Server.Customers.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<Guid, string>>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Street { get; }
        public string StreetNumber { get; }
        public string Town { get; }
        public string Zipcode { get; }

        public CreateCustomerCommand(
            string firstName,
            string lastName, 
            string email, 
            string street, 
            string streetNumber,
            string town, 
            string zipcode )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            StreetNumber = streetNumber;
            Town = town;
            Zipcode = zipcode;
        }
    }
}
