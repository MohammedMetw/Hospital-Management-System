using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Features.Departments.Queries;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Doctor.Queries
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

          public async Task<DoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.Id);

            if (doctor == null) 
            {
                return null;
            }
            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Phone = doctor.Phone,
                DepartmentId = doctor.DepartmentId
            };

        }
    }
}
