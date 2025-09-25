using Hospital.Application.Features.Dashboard.Queries;
using Hospital.Application.Features.Doctor.Queries;
using Hospital.Application.Features.User.Command;
using Hospital.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }



        [HttpPost("AssignRole/{userId}")]
        public async Task<IActionResult> AssignRole(string userId, [FromBody] string roleName)
        {
            var result = await _mediator.Send(new AssignRoleToUserCommand
            {
                UserId = userId,
                RoleName = roleName
            });

            return Ok("Role assigned successfully");
        }

        [HttpDelete("RemoveRole/{userId}")]
        public async Task<IActionResult> RemoveRole(string userId, [FromBody] string roleName)
        {
            var result = await _mediator.Send(new RemoveRoleFromUserCommand
            {
                UserId = userId,
                RoleName = roleName
            });
           
            return Ok("Role removed successfully");
        }

        [HttpPut("LockUser/{userId}")]
        public async Task<IActionResult> LockUser(string userId)
        {
            var result = await _mediator.Send(new lockUserCommand {  Id = userId });
            return Ok(result);
        }

        [HttpPut("UnlockUser/{userId}")]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            var result = await _mediator.Send(new UnlockUserCommand { Id = userId });
            return Ok(result);
        }

        [HttpGet("DachboardStat")]
        public async Task<IActionResult> DachboardStat()
        {
            var result = await _mediator.Send(new GetDashboardStatsQuery());
            return Ok(result);
        }


    }
}
