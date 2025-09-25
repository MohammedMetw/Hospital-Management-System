using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Register.Command
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterUserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
       

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            
        }

        public async Task<RegisterUserResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            
          

          
            var existingUser = await _userManager.FindByEmailAsync(command.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User already exists with this email.");

           
            var nameParts = command.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.FirstOrDefault() ?? "";
            var lastName = nameParts.Skip(1).FirstOrDefault() ?? "";

            var user = new ApplicationUser
            {
                UserName = command.Email,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Registration failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            return new RegisterUserResponse
            {
                
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = "Patient", //Default role assignment
                PhoneNumber =user.PhoneNumber

            };
        }
    }
}
