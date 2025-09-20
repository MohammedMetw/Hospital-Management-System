using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Doctor.Command
{
    internal class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }

            doctor.ApplicationUser.FirstName = request.FirstName;
            doctor.ApplicationUser.LastName = request.LastName;
            doctor.ApplicationUser.PhoneNumber = request.Phone;
           // doctor.ApplicationUser.Email = request.Email;
            doctor.Specialty = request.Specialty;
            doctor.DepartmentId = request.DepartmentId;

            await _unitOfWork.Doctors.UpdateAsync(doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                Email = doctor.ApplicationUser.Email,
                Phone = doctor.ApplicationUser.PhoneNumber,
                Specialty = doctor.Specialty,
                DepartmentId = doctor.DepartmentId
            };
        }

    }
}
