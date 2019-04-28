using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nehft.Server.Customers.CreateCustomer;
using Nehft.Server.Customers.GetCustomer;
using Nehft.Server.Horses.AddHorse;

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
        [Route("/api/customer/addHorse")]
        public async Task<IActionResult> AddHorse(AddHorseDto dto)
        {
            var command = AddHorseCommand.Create(dto.Customer, dto.Name, dto.Type, dto.Breed, dto.YearOfBirth, dto.Exterior, dto.History, dto.Street, dto.HouseNumber, dto.Town, dto.ZipCode);

            return await command.OnBoth<Task<ActionResult>>(async x =>
                {
                    await _mediator.Send(x);
                    //TODO: What should be returned?
                    return Ok();
                },
                async error => await Task.FromResult(NotFound(error)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            return result.OnBoth<ObjectResult>(
                x => Created("/api/customer", x),
                BadRequest);
        }
    }
}