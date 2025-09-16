using Hospital.Application.Features.Doctor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Nurse.Queries;
using Hospital.Application.Features.Nurse.Command;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NurseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllNurses")]
        public async Task<IActionResult> GetAllNurses()
        {
            var Nurses= await _mediator.Send(new GetAllNuresQuery());
            return Ok(Nurses);
        }
        [HttpGet]
        [Route("GetNurseByID/{id}")]
        public async Task<IActionResult> GetNurseByID(int id)
        {
            var nurse = await _mediator.Send(new GetNurseByIDQuery(id));
            return Ok(nurse);
        }
        [HttpPost]
        [Route("CreateNurse")]
        public async Task<IActionResult> CreateNurse([FromBody] CreateNurseCommand command)
        {
            var newNurse = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetNurseByID), new { id = newNurse }, null);
        }
        [HttpPut("UpdateNurse/{id}")]
        public async Task<IActionResult> UpdateNurse(int id, [FromBody] UpdateNurseCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }
            var updatedNurseDto = await _mediator.Send(command);
            return Ok(updatedNurseDto);
        }
        [HttpDelete("DeleteNurse/{id}")]
        public async Task<IActionResult> DeleteNurse(int id)
        {
            var deletedNurse = await _mediator.Send(new DeleteNurseCommand (id));

            return Ok($"Nurse with ID {id} was deleted successfully.");

        }
    }
}
