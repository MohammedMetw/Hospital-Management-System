using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hospital.Application.Features.Departments.Queries;
using Hospital.Application.Features.Departments.Command;
namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartment()
        {

            var departments = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }
        [HttpGet("GetDepatmentByID/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _mediator.Send(new GetDepartmentByIDQuery(id));
            return Ok(department);
        }
        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var newDepartment = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment }, null);
        }
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }
            var department = await _mediator.Send(command);
            return Ok(department);
        }
        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _mediator.Send(new DeleteDepartmentCommand { Id = id });
            return NoContent();
        }
        [HttpGet("GetAllDoctorsInSpecificDepartment/{departmentId}")]
        public async Task<IActionResult> GetAllDoctorsInSpecificDepartment(int departmentId)
        {
            var doctors = await _mediator.Send(new GetAllDoctorsInSpecificDepartmentCommand(departmentId));
            return Ok(doctors);
        }
    }
}
