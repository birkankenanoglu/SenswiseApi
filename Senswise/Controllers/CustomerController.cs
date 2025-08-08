using Microsoft.AspNetCore.Mvc;
using Senswise.API.Request;
using Senswise.Application;
using Senswise.Infra;

namespace Senswise.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerCommandHandler _commandHandler;
        private readonly CustomerQueryHandler _queryHandler;


        public CustomerController(CustomerCommandHandler commandHandler, CustomerQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            var id = await _commandHandler.Handle(new CreateCustomerCommand() { Address = request.Address, Email = request.Email, FirstName = request.FirstName, LastName = request.LastName });
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _queryHandler.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commandHandler.HandleDeleteById(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerCommand command)
        {
            if (id != command.Id) return BadRequest("ID uyuşmuyor.");

            var result = await _commandHandler.Handle(command);
            if (!result) return NotFound();

            return Ok();
        }
    }
}
