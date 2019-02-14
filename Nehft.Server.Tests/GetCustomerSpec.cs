using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Nehft.Server.Customers.GetCustomer;
using Newtonsoft.Json;
using Xunit;

namespace Nehft.Server.Tests
{
    public class GetCustomerSpec : SpecBase<GetCustomerSpec>
    {
        private GetCustomerResult _customerInfo;

        public GetCustomerSpec(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public void Can_retrieve_specific_customer_information()
        {
            var customerId = Guid.NewGuid();
            var name = Name.Create("firstName", "lastName").Value();
            var address = Address.Create("street", "4A", "town", "zippy").Value();
            var emailAddress = EmailAddress.Create("my@awesome.email").Value();
            var customer = new Customer(customerId, name, emailAddress, address);

            Given
                .a_customer_is_persisted(customer);

            When
                .a_client_request_customer_info_for(customerId);

            Then
                .customer_info_is_returned("firstName", "lastName", "my@awesome.email");
        }

        private void customer_info_is_returned(string firstName, string lastName, string email)
        {
            Assert.Equal(_customerInfo.FirstName, firstName);
            Assert.Equal(_customerInfo.LastName, lastName);
            Assert.Equal(_customerInfo.Email, email);
        }

        private void a_client_request_customer_info_for(Guid customerId)
        {
            var response = Client.GetAsync($"/api/customer/{customerId}").Result;
            response.EnsureSuccessStatusCode();
            _customerInfo = JsonConvert.DeserializeObject<GetCustomerResult>(response.Content.ReadAsStringAsync().Result);
        }

        private void a_customer_is_persisted(Customer customer)
        {
            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                repository.Save(customer);
            }
        }
    }
}