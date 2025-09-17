using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Doctor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Accountant.Queries;
using Hospital.Application.Features.Accountant.Command;
using System.Threading.Tasks;

namespace Hospital.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllAccountant")]
        public async Task<IActionResult> GetAllAccountants()
        {
            var Accountants = await _mediator.Send(new GetAllAccountantsQuery());
            return Ok(Accountants);

        }
        [HttpGet]
        [Route("GetAccountantByID/{id}")]
        public async Task<IActionResult> GetAccountantByID(int id)
        {
            var accountant = await _mediator.Send(new GetAccountantByIDQuery(id));
            return Ok(accountant);
        }
        [HttpPost]
        [Route("CreateAccountant")]
        public async Task<IActionResult> CreateAccountant([FromBody] CreateAccountantCommand command)
        {
            var newAccountant = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAccountantByID), new { id = newAccountant }, null);
        }
        [HttpPut("UpdateAccountant/{id}")]
        public async Task<IActionResult> UpdateAccountant(int id, [FromBody] UpdateAccountantCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }
            var updatedAccountantDto = await _mediator.Send(command);
            return Ok(updatedAccountantDto);
        }
        [HttpDelete("DeleteAccountant/{id}")]
        public async Task<IActionResult> DeleteAccountant(int id)
        {
            var deletedAccountant = await _mediator.Send(new DeleteAccountantCommand(id));
            return Ok($"Accountant with ID {id} was deleted successfully.");
        }
    }
}
