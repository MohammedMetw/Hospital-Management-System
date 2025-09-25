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
    public class GetAllAppointmentForAllPaitentQueryHandler : IRequestHandler<GetAllAppointmentForAllPaitentQuery,IEnumerable <AppointmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentForAllPaitentQueryHandler(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentForAllPaitentQuery request, CancellationToken cancellationToken)
        {
            var appointements =await _unitOfWork.Appointments.GetAllAsync();

            var appointmentsDtos = appointements.Select(x => new AppointmentDto
            {
                Id = x.Id,
                PatientName = $"{x.patient?.ApplicationUser?.FirstName} {x.patient?.ApplicationUser?.LastName}",
                DoctorName = $"{x.doctor?.ApplicationUser?.FirstName} {x.doctor?.ApplicationUser?.LastName}",
                Date = x.Date,
                Status = x.Status,
                DoctorId = x.DoctorId,
                PatientId = x.PatientId
            });
            return appointmentsDtos;

        }
    }
}
