using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Hospital.Application.Features.Appointment.Command;
using Hospital.Application.Features.Appointment.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("CancelAppointment{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _mediator.Send(new CancelAppointmentCommand { AppointmentId = id });
            return Ok(result);
        }

        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var result = await _mediator.Send(new GetAllAppointmentForAllPaitentQuery());
            return Ok(result);
        }

        [HttpGet("GetAppointmentsForPatitent{id}")]
        public async Task<IActionResult> GetAppointmentsForPatient(int id)
        {
            var result = await _mediator.Send(new GetAllAppointmentQuery { PatientId = id });
            return Ok(result);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet("GetAppointmentById{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var result = await _mediator.Send(new GetAppointmentByIdQuery { AppointmentId = id });
            return Ok(result);
        }


    }
}
