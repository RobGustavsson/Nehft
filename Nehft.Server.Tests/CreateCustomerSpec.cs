using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Nehft.Server.Customers.CreateCustomer;
using Xunit;

namespace Nehft.Server.Tests
{
    public class CreateCustomerSpec : SpecBase<CreateCustomerSpec>
    {
        private HttpResponseMessage _response;

        [Fact]
        public void A_customer_can_be_created()
        {

            var createCustomerCommand = new CreateCustomerCommand(
                "first name",
                "last name",
                "my@email.com",
                "the street",
                "4A",
                "town",
                "zipcode");

            When
                .a_client_posts_a_create_customer_command(createCustomerCommand);

            Then
                .a_customer_is_persisted(createCustomerCommand);
        }

        [Fact]
        public void Cannot_create_invalid_customer()
        {

            var invalidCommand = new CreateCustomerCommand(
                "",
                "last name",
                "my@email.com",
                "the street",
                "4A",
                "town",
                "zipcode");

            When
                .a_client_posts_a_create_customer_command(invalidCommand);

            Then
                .the_server_returns_403()
                .the_customer_is_not_persisted();
        }

        private CreateCustomerSpec the_server_returns_403()
        {
            Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
            return this;
        }

        private void the_customer_is_not_persisted()
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var customers = repository.GetAll();
                Assert.Empty(customers);
            }
        }

        private void a_customer_is_persisted(CreateCustomerCommand command)
        {
            using(var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

                var customer = repository.GetAll().Single();

                Assert.Equal(customer.Name.FirstName, command.FirstName);
                Assert.Equal(customer.Name.LastName, command.LastName);
                Assert.Equal(customer.Email.Email, command.Email);
                Assert.Equal(customer.Address.Number, command.StreetNumber);
                Assert.Equal(customer.Address.Street, command.Street);
                Assert.Equal(customer.Address.Town, command.Town);
                Assert.Equal(customer.Address.Zipcode, command.Zipcode);

            }
        }

        private void a_client_posts_a_create_customer_command(CreateCustomerCommand createCustomerCommand)
        {
            _response = Client.PostAsJsonAsync("/api/customer", createCustomerCommand).Result;
        }

        public CreateCustomerSpec(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }
    }
}
