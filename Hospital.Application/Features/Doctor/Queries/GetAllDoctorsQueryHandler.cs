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
    internal class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {

            var doctors = await _doctorRepository.GetAllAsync();

             
            var doctorsDtos = doctors.Select(doc => new DoctorDto
            {
                Id = doc.Id,
                FullName = $"{doc.ApplicationUser.FirstName} {doc.ApplicationUser.LastName}",
                Email=doc.Email,
                Phone=doc.Phone,
                Specialty = doc.Specialty,
                DepartmentId = doc.DepartmentId

            }).ToList();


            return doctorsDtos;
        }
    }
}
