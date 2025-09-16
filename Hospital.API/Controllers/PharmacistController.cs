using Hospital.Application.Features.Pharmacist.Command;
using Hospital.Application.Features.Pharmacist.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PharmacistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllPharmacist")]
        public async Task<IActionResult> GetAllPharmacist()
        {
            var responce = await _mediator.Send(new GetAllPharmacistQuery());
            return Ok(responce);
        }

        [HttpGet("GetPharmacistByID/{id}")]
        public async Task<IActionResult> GetPharmacistByID(int id)
        {
            var responce = await _mediator.Send(new GetPharmacistByIdQuery { Id = id });
            return Ok(responce);
        }

        [HttpPost("CreatePharmacist")]
        public async Task<IActionResult> CreatePharmacist([FromBody] CreatePharmacistCommand command)
        {
            var newPharmacistId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPharmacistByID), new { id = newPharmacistId}, null);
        }

        [HttpPut("UpdatePharmacist/{id}")]
        public async Task<IActionResult> UpdatePharmacist(int id, [FromBody] UpdatePharmacistCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }

            var UpdatePharmacistPrpfile =await _mediator.Send(command);
            return Ok(UpdatePharmacistPrpfile);
        }

         [HttpDelete("DeletePharmacist/{id}")]
         public async Task<IActionResult> DeletePharmacist(int id)
        {
            var DeletePharmacist = await _mediator.Send(new DeletePharmacistCommand { Id = id });
            return Ok($"Pharmacist '{DeletePharmacist}' was deleted successfully.");
        }

    }
}
