using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Prescription.Command
{
    public class CreatePrescriptionCommand : IRequest<int>
    {
        
        public int DoctorId { get; set; }
       
        public int PatientId { get; set; }

        public required List<PrescribedMedicineDto> PrescribedMedicines { get; set; }
        
    }
}
