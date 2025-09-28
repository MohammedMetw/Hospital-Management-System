using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Features.Appointment.Command
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, AppointmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContextService _userContext;
        public BookAppointmentCommandHandler(IUserContextService userContext,IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager) 
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<AppointmentDto> Handle(BookAppointmentCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = _userContext.GetUserId();
            var currentUserRole = _userContext.GetUserRole();

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(command.DoctorId);
            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }
            var patient = await _unitOfWork.Patients.GetByIdAsync(command.PatientId);
            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }

            //if (currentUserRole != "Admin" && !(currentUserRole == "Patient" && patient.ApplicationUserId == currentUserId))
            //{
            //    throw new UnauthorizedAccessException("You are not authorized to book an appointment for this patient.");
            //}


            var newAppointment = new Domain.Entities.Appointment
            {
                Date = command.Date,
                Status = "Pending",
                DoctorId = command.DoctorId,
                PatientId = command.PatientId
            };

            await _unitOfWork.Appointments.AddAsync(newAppointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return new AppointmentDto
            {
                Id = newAppointment.Id,
                Date = newAppointment.Date,
                Status = newAppointment.Status,
                DoctorId = doctor.Id,
                DoctorName = $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}",
                PatientId = patient.Id,
                PatientName = $"{patient.ApplicationUser.FirstName} {patient.ApplicationUser.LastName}"
            };


        }

    }
}
