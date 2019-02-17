using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Nehft.Server.Customers.AddAnimal;
using Xunit;

namespace Nehft.Server.Tests
{
    public class AddAnimalSpec : SpecBase<AddAnimalSpec>
    {
        [Fact]
        public void Users_can_add_animals_to_customers()
        {
            var customerId = Guid.NewGuid();
            var name = Name.Create("firstName", "lastName").Value();
            var address = Address.Create("street", "4A", "town", "zippy").Value();
            var emailAddress = EmailAddress.Create("my@awesome.email").Value();
            var customer = new Customer(customerId, name, emailAddress, address);

            var addAnimalCommand = new AddAnimalCommand(customerId,"animal name", "horse");

            Given
                .a_customer(customer);

            When
                .the_user_adds_an_animal_to_the_customer(addAnimalCommand);

            Then
                .the_animal_is_assigned_to_the_customer(customerId, "animal name", "horse");

        }

        private void the_animal_is_assigned_to_the_customer(Guid customerId, string name, string type)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var customer = repository.Get(customerId);

                var animal = customer.Animals.Single();
                Assert.Equal(name, animal.Name);
                Assert.Equal(type, animal.Type);
            }
        }


        private void the_user_adds_an_animal_to_the_customer(AddAnimalCommand command)
        {
            var response = Client.PostAsJsonAsync("/api/customer/addAnimal", command).Result;
            response.EnsureSuccessStatusCode();
        }

        private void a_customer(Customer customer)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                repository.Save(customer);
            }
        }

        public AddAnimalSpec(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }
    }
}