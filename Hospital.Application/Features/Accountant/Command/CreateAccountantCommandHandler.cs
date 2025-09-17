using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Accountant.Command
{
   public class CreateAccountantCommandHandler : IRequestHandler<CreateAccountantCommand, AccountantDto>

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAccountantCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        async Task<AccountantDto> IRequestHandler<CreateAccountantCommand, AccountantDto>.Handle(CreateAccountantCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email
            };
            var Accountant = new Domain.Entities.Accountant
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                
                ApplicationUserId = user.Id,

               
            };
            var result = _userManager.CreateAsync(user, request.Password).Result;
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
           await _userManager.AddToRoleAsync(user, "Accountant");
           await _unitOfWork.Accountants.AddAsync(Accountant);
            await _unitOfWork.SaveChangesAsync();
            var AccountantDto = new AccountantDto
            {
                Id = Accountant.Id, 
                FullName = $"{user.FirstName} {user.LastName}",
                Email = Accountant.Email,
                PhoneNumber = Accountant.PhoneNumber,

            };
            return AccountantDto;


        }
    }
}
