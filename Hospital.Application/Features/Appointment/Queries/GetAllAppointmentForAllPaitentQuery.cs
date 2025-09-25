using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Appointment.Queries
{
    public class GetAllAppointmentForAllPaitentQuery : IRequest<IEnumerable <AppointmentDto>>
    {
    }
}
