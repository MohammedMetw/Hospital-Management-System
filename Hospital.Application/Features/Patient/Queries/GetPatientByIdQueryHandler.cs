using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Patient.Queries
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PatientsDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(request.Id);
            if (patient == null)
            {
                throw new Exception("Patient not found");
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
