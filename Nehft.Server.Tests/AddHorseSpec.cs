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

            var addHorseDto = new AddHorseDto(
                customerId,
                "animal name",
                "stallion",
                "best horse breed",
                1989, 
                "a bit fat", 
                "never been sick", 
                "the street", 
                "1", 
                "the town", 
                "zippy");

            Given
                .a_customer(customer);

            When
                .the_user_adds_a_horse_to_the_customer(addHorseDto);

            Then
                .the_horse_is_persisted(addHorseDto)
                .the_horse_is_assigned_to_the_customer(customerId);
        }

        private AddHorseSpec the_horse_is_persisted(AddHorseDto dto)
        {
            var horseType = Enum.Parse<HorseType>(dto.Type, true);
            var address = Address.Create(dto.Street, dto.HouseNumber, dto.Town, dto.ZipCode).Value();

            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IHorseRepository>();
                var actualHorse = repository.GetAll().Single();

                Assert.Equal(dto.Name, actualHorse.Name);
                Assert.Equal(horseType, actualHorse.Type);
                Assert.Equal(dto.Breed, actualHorse.Breed);
                Assert.Equal(dto.YearOfBirth, actualHorse.YearOfBirth);
                Assert.Equal(dto.History, actualHorse.History);
                Assert.Equal(dto.Exterior, actualHorse.Exterior);
                Assert.Equal(address, actualHorse.Address);

            }

            return this;
        }

        private void the_horse_is_assigned_to_the_customer(Guid customerId)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var animalRepository = scope.ServiceProvider.GetRequiredService<IHorseRepository>();
                var customerAnimalId = customerRepository.Get(customerId).Horses.Single();
                var storedAnimalId = animalRepository.GetAll().Single().Id;

                Assert.Equal(storedAnimalId, customerAnimalId);
            }
        }

        private void the_user_adds_a_horse_to_the_customer(AddHorseDto dto)
        {
            var response = Client.PostAsJsonAsync("/api/customer/addHorse", dto).Result;
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