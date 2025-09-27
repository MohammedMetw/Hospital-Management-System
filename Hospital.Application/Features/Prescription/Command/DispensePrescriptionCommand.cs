using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Prescription.Command
{
    public class DispensePrescriptionCommand : IRequest<BillDto>
    {
       
        public int PrescriptionId { get; set; }

        
        public int PharmacistId { get; set; }
    }
}
