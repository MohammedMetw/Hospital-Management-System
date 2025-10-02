using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Hospital.Domain.Entities;
using Microsoft.Data.SqlClient;
using Hospital.Application.Features.Login.Command;
using Hospital.Application.Features.Register.Command;
using Hospital.Application.Interfaces;
using Hospital.Application.DTOs;
using Hospital.Application.Features.Logout;
using Microsoft.AspNetCore.Authorization;
using Hospital.Application.Features.ChangePassword.Command;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
       
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
            

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var loginResponse = await _mediator.Send(command);
            return Ok(loginResponse);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var registerResponse = await _mediator.Send(command);
            return Ok(registerResponse);
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Email confirmed successfully.");
            }
            return BadRequest("Email could not be confirmed.");
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return Ok("Successfully logged out.");
        }




    }
}
