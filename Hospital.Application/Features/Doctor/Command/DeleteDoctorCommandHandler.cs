using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }
            await _unitOfWork.Doctors.DeleteAsync(doctor);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DoctorDto
            {
              
                FullName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                Id = doctor.Id

            };
            

        }

    }
}
