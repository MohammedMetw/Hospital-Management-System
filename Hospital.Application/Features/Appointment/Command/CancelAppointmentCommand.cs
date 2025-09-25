using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Appointment.Command
{
    public class CancelAppointmentCommand : IRequest<AppointmentDto>
    {
        public int AppointmentId { get; set; }
    }
}
