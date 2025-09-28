using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Features.Appointment.Command
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, AppointmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;

        public CancelAppointmentCommandHandler(IUnitOfWork unitOfWork , IUserContextService userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<AppointmentDto> Handle(CancelAppointmentCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = _userContext.GetUserId();
            var currentUserRole = _userContext.GetUserRole();

            
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(command.AppointmentId);
            if (appointment == null)
            {
                throw new NotFoundException("Appointment not found");
            }
            bool isPatientOwner = currentUserRole == "Patient" && appointment.patient.ApplicationUserId == currentUserId;
            bool isDoctorOwner = currentUserRole == "Doctor" && appointment.doctor.ApplicationUserId == currentUserId;

            if (currentUserRole != "Admin" && !isPatientOwner && !isDoctorOwner)
            {
                throw new UnauthorizedAccessException("You are not authorized to cancel this appointment.");
            }

            appointment.Status = "Cancelled";
            await _unitOfWork.Appointments.UpdateAsync(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AppointmentDto
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Status = appointment.Status,
                DoctorId = appointment.Id,
                DoctorName = $"{appointment.doctor.ApplicationUser.FirstName} {appointment.doctor.ApplicationUser.LastName}",
                PatientId = appointment.Id,
                PatientName = $"{appointment.patient.ApplicationUser.FirstName} {appointment.patient.ApplicationUser.LastName}"

            };


        }
    }
}
