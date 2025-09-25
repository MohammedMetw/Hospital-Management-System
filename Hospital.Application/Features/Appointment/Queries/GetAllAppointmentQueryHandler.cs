using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Features.Appointment.Queries
{
    public class GetAllAppointmentQueryHandler : IRequestHandler<GetAllAppointmentQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAppointmentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentQuery request, CancellationToken cancellationToken)
        {
             var patient = await _unitOfWork.Patients.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }


            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(request.PatientId);
            return appointments.Select(x => new AppointmentDto
            {
                Id = x.Id,
                PatientName = $"{x.patient?.ApplicationUser?.FirstName} {x.patient?.ApplicationUser?.LastName}",
                DoctorName = $"{x.doctor?.ApplicationUser?.FirstName} {x.doctor?.ApplicationUser?.LastName}",
                Date = x.Date,
                Status = x.Status,
                DoctorId = x.DoctorId,
                PatientId = x.PatientId
            });

        }
    }
}
