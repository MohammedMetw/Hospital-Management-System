using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace Hospital.Application.Features.Login.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponce>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginCommandHandler> _logger;


        public LoginCommandHandler(ILogger<LoginCommandHandler> logger,UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            
        }

        public async Task<LoginResponce> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to log in user {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogWarning("Login failed for user {Email}", request.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                _logger.LogWarning("Login failed for user {Email}", request.Email);
                throw new UnauthorizedAccessException("Email is not confirmed.");
            }
            _logger.LogInformation("User {Email} logged in successfully", request.Email);
            var token = await _tokenService.CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            

            return new LoginResponce
            {
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Roles = roles,
                Token = token
                
            };
        }
    }
}
