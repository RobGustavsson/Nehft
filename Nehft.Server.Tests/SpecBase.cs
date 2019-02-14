using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Nehft.Server.Customers;
using Xunit;

namespace Nehft.Server.Tests
{
    public abstract class SpecBase<T> : IClassFixture<WebApplicationFactory<Startup>> where T : SpecBase<T>
    {
        protected WebApplicationFactory<Startup> Factory { get; }
        protected HttpClient Client { get; }


        protected SpecBase(WebApplicationFactory<Startup> factory)
        {
            Factory = factory;

            Client = Factory.CreateClient();

            using (var scope = Factory.Server.Host.Services.CreateScope())
            {
                var repository = (InMemoryCustomerRepository)scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                repository.Clear();
            }
        }


        private T This => (T) this;

        protected T Given => This;
        protected T When => This;
        protected T Then => This;
    }
}