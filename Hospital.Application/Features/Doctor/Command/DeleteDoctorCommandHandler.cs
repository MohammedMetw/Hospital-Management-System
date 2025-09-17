using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Doctor.Command
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DoctorDto> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }
            await _unitOfWork.Doctors.DeleteAsync(doctor);
            await _userManager.RemoveFromRoleAsync(doctor.ApplicationUser, "Doctor");
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DoctorDto
            {
              
                FullName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                Id = doctor.Id

            };
            

        }

    }
}
