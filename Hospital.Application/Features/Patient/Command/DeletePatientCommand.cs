using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Patient.Command
{
    public class DeletePatientCommand : IRequest<Unit>
    {
        public int Id { get; set; } 
    }
}
