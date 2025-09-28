using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Patient.Command
{
    public class UpdatePatientCommandHandler: IRequestHandler<UpdatePatientCommand, PatientsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PatientsDto> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(command.Id);
            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }
            patient.ApplicationUser.FirstName = command.FirstName;
            patient.ApplicationUser.LastName = command.LastName;
            patient.ApplicationUser.Email = command.Email;
            patient.Email = command.Email;
            patient.ApplicationUser.PhoneNumber = command.Phone;
            patient.Address = command.Address;
            patient.City = command.City;
            patient.Gender = command.Gender;
            patient.DateOfBirth = command.DateOfBirth;
           
            await _unitOfWork.SaveChangesAsync();
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
