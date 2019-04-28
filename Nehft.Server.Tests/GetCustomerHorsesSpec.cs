using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Nehft.Server.Customers.GetHorses;
using Nehft.Server.Horses;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Nehft.Server.Tests
{
    public class GetCustomerHorsesSpec : SpecBase<GetCustomerHorsesSpec>
    {
        private IEnumerable<GetCustomerHorsesResult> _customerHorses;

        public GetCustomerHorsesSpec(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void Get_access_all_customer_horses()
        {
            var customerId = Guid.NewGuid();
            var name = Name.Create("firstName", "lastName").Value();
            var address = Address.Create("street", "4A", "town", "zippy").Value();
            var emailAddress = EmailAddress.Create("my@awesome.email").Value();
            var customer = new Customer(customerId, name, emailAddress, address);
            var customer2 = new Customer(Guid.NewGuid(), name, emailAddress, address);

            Guid horse1Id = Guid.NewGuid();
            Guid horse2Id = Guid.NewGuid();
            var horse = new Horse(horse1Id, "horse1", HorseType.Gelding, "breed", "exterior", "history", 1989, address);        
            var horse2 = new Horse(horse2Id, "horse2", HorseType.Mare, "breed", "exterior", "history", 1990, address);
            var horse3 = new Horse(Guid.NewGuid(), "horse3", HorseType.Stallion, "breed", "exterior", "history", 1991, address);

            Given
                .a_customer_with_existing_horses(customer, horse, horse2)
                .a_customer_with_existing_horses(customer2, horse3);

            When
                .a_client_requests_a_customers_horses(customerId);

            Then
                .all_customer_horses_are_returned(
                    (horse1Id, "horse1", HorseType.Gelding),
                    (horse2Id, "horse2", HorseType.Mare));

        }

        private void all_customer_horses_are_returned(params (Guid Id, string Name, HorseType Type)[] results)
        {
            var expectation = results.Select(x => new GetCustomerHorsesResult(x.Id, x.Name, x.Type));
            _customerHorses.Should().BeEquivalentTo(expectation);
        }

        private void a_client_requests_a_customers_horses(Guid customerId)
        {
            var response = Client.GetAsync($"/api/customer/horses/{customerId}").Result;
            response.EnsureSuccessStatusCode();
            _customerHorses = JsonConvert.DeserializeObject<IEnumerable<GetCustomerHorsesResult>>(response.Content.ReadAsStringAsync().Result);
        }

        private GetCustomerHorsesSpec a_customer_with_existing_horses(Customer customer, params Horse[] horses)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var animalRepository = scope.ServiceProvider.GetRequiredService<IHorseRepository>();
                
                foreach(var horse in horses)
                {
                    animalRepository.Save(horse);
                    customer.AddHorse(horse.Id);
                }

                customerRepository.Save(customer);
            }

            return this;
        }
    }
}
