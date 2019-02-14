using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nehft.Server.Customers.CreateCustomer;
using Nehft.Server.Customers.GetCustomer;

namespace Nehft.Server.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/api/customer/{customerId}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var customer = await _mediator.Send(new GetCustomerQuery(customerId));
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            return result.OnBoth<ObjectResult>(
                x => Created("/api/customer", x),
                BadRequest);


        }
    }
}