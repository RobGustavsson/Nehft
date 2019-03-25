using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Nehft.Server.Horses;
using Nehft.Server.Horses.AddHorse;
using Xunit;

namespace Nehft.Server.Tests
{
    public class AddHorseSpec : SpecBase<AddHorseSpec>
    {
        [Fact]
        public void Users_can_add_animals_to_customers()
        {
            var customerId = Guid.NewGuid();
            var name = Name.Create("firstName", "lastName").Value();
            var address = Address.Create("street", "4A", "town", "zippy").Value();
            var emailAddress = EmailAddress.Create("my@awesome.email").Value();
            var customer = new Customer(customerId, name, emailAddress, address);

            var addHorseCommand = new AddHorseCommand(customerId,"animal name", "horse");

            Given
                .a_customer(customer);

            When
                .the_user_adds_a_horse_to_the_customer(addHorseCommand);

            Then
                .the_horse_is_persisted("animal name", "horse")
                .the_horse_is_assigned_to_the_customer(customerId);
        }

        private AddHorseSpec the_horse_is_persisted(string horseName, string horseType)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IHorseRepository>();
                var actualHorse = repository.GetAll().Single();

                Assert.Equal(horseName, actualHorse.Name);
                Assert.Equal(horseType, actualHorse.Type);
            }

            return this;
        }

        private void the_horse_is_assigned_to_the_customer(Guid customerId)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var animalRepository = scope.ServiceProvider.GetRequiredService<IHorseRepository>();
                var customerAnimalId = customerRepository.Get(customerId).Animals.Single();
                var storedAnimalId = animalRepository.GetAll().Single().Id;

                Assert.Equal(storedAnimalId, customerAnimalId);
            }
        }

        private void the_user_adds_a_horse_to_the_customer(AddHorseCommand command)
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

        public AddHorseSpec(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }
    }
}