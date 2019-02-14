using AutoMapper;
using Nehft.Server.Customers.GetCustomer;

namespace Nehft.Server.Customers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomerResult>()
                .ForCtorParam("firstName", x => x.MapFrom(y => y.Name.FirstName))
                .ForCtorParam("lastName", x => x.MapFrom(y => y.Name.LastName))
                .ForCtorParam("email", x => x.MapFrom(y => y.Email.Email));
        }
    }
}