using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Appointment.Queries
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            return new AppointmentDto
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Status = appointment.Status,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                PatientName = $"{appointment.patient?.ApplicationUser?.FirstName} {appointment.patient?.ApplicationUser?.LastName}",
                DoctorName = $"{appointment.doctor?.ApplicationUser?.FirstName} {appointment.doctor?.ApplicationUser?.LastName}",
            };
        }
    }
}
