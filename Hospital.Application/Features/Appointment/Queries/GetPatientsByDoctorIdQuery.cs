using Hospital.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Hospital.Application.Features.Doctor.Queries
{
    public class GetPatientsByDoctorIdQuery : IRequest<IEnumerable<AppointmentDto>>
    {
        public int DoctorId { get; set; }

      
    }
}

