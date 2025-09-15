using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Features.Departments.Queries;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Doctor.Queries
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

          public async Task<DoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id);

            if (doctor == null) 
            {
                throw new Exception("Doctor not found");
            }
            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                Specialty = doctor.Specialty,
                Email = doctor.ApplicationUser.Email,
                Phone = doctor.ApplicationUser.PhoneNumber,
                DepartmentId = doctor.DepartmentId
            };

        }
    }
}
