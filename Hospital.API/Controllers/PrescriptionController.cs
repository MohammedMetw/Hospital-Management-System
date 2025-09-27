using Hospital.Application.Features.Prescription.Command;
using Hospital.Application.Features.Prescription.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrescriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create-Prescription")]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var response = await _mediator.Send(new GetPrescriptionByIdQuery { PrescriptionId = id });
            return Ok(response);
        }

        [HttpPost("prescriptions/dispense")]
       // [Authorize(Roles = "Pharmacist")]
        public async Task<IActionResult> DispensePrescription([FromBody] DispensePrescriptionCommand command)
        {
            var bill = await _mediator.Send(command);
            return Ok(bill);
        }


    }
}
