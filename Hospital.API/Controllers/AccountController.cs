using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Hospital.Domain.Entities;
using Microsoft.Data.SqlClient;
using Hospital.Application.Features.Login.Command;
using Hospital.Application.Features.Register.Command;

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



    }
}
