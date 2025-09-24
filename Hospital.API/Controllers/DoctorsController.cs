using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Doctor.Queries; 
using Hospital.Application.Features.Doctor.Command;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _mediator.Send(new GetAllDoctorsQuery());
            return Ok(doctors);
        }


        [HttpGet("GetDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _mediator.Send(new GetDoctorByIdQuery { Id = id });
            return Ok(doctor);
        }

        [HttpPost]
        [Route("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorCommand command)
        {
            var newDoctor = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDoctorById), new { id = newDoctor }, null);
        }

        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }

            var updatedDoctorDto = await _mediator.Send(command);
            return Ok(updatedDoctorDto);
        }


        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            
            var deletedDoctorName = await _mediator.Send(new DeleteDoctorCommand { Id = id });
            return Ok($"Doctor '{deletedDoctorName}' was deleted successfully.");
        }





    }
}
