using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Patient.Queries
{
    public class GetPatientByNameQueryHandler : IRequestHandler<GetPatientByNameQuery, PatientsDto>
    {
        private readonly IUnitOfWork _unitofWork;
        public GetPatientByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        public async Task<PatientsDto> Handle(GetPatientByNameQuery request, CancellationToken cancellationToken)
        {
            var patient = await _unitofWork.Patients.GetPatientByName(request.FullName);
            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }
            return new PatientsDto
            {
                Id = patient.Id,
                FullName = $"{patient.ApplicationUser.FirstName} {patient.ApplicationUser.LastName}",
                Gender = patient.Gender,
                Email = patient.Email,
                Phone = patient.Phone,
                Address = patient.Address,
                City = patient.City,
                DateOfBirth = patient.DateOfBirth

            };
        }
    }
}
