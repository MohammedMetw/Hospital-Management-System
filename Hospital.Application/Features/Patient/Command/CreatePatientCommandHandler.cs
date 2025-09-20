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

namespace Hospital.Application.Features.Patient.Command
{
    public class CreatePatientCommandHandler  : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;    
        public CreatePatientCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> Handle (CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var newUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone

            };
           
            var newPatientProfile = new Domain.Entities.Patient
            {
                ApplicationUserId = newUser.Id,
                Gender = request.Gender,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {

                throw new InvalidOperationException("Failed to create user. " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(newUser, "Patient");
            await _unitOfWork.Patients.AddAsync(newPatientProfile);
            await _unitOfWork.SaveChangesAsync();

            return newPatientProfile.Id;

        }
    }
}
