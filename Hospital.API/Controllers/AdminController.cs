using Hospital.Application.Features.Doctor.Queries;
using Hospital.Application.Features.User.Command;
using Hospital.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpDelete("HardDeleteUser/{userId}")]
        public async Task<IActionResult> HardDeleteUser(string userId)
        {
            var command = new HardDeleteUserCommand { UserId = userId };
            await _mediator.Send(command);

            
            return NoContent();
        }

       
    }
}
