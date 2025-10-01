using Hospital.Application.DTOs;
using Hospital.Application.Features.Doctor.Queries;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;
using System;

namespace Hospital.Application.Features.Appointment.Queries
{
    public class GetPatientsByDoctorIdQueryHandler : IRequestHandler<GetPatientsByDoctorIdQuery, IEnumerable<AppointmentDto>>
    {
        private readonly  IUnitOfWork _unitofwork ;

        public GetPatientsByDoctorIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        
        public async Task<IEnumerable<AppointmentDto>> Handle(GetPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var DoctorId =_unitofwork.Doctors.GetByIdAsync(request.DoctorId);
            if (DoctorId == null)
            {
                throw new Exception("Doctor not found");
            }
            var result = await _unitofwork.Appointments.GetPatientAppointmentsById(request.DoctorId);
            var Appointment = result.Select(a => new AppointmentDto
            {
                PatientName = $"{a.patient.ApplicationUser.FirstName} {a.patient.ApplicationUser.LastName}",
                Date = a.Date,
                Status=a.Status
            });
            return Appointment;
                

        }
    }

}

