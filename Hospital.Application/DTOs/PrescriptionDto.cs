using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.DTOs
{
    public class PrescriptionDto
    {
        public int PrescriptionId { get; set; }
        public DateTime PrescriptionDate { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }


        public int PatientId { get; set; }
        public string PatientName { get; set; }

       
        public List<PrescribedMedicineDto> PrescribedMedicines { get; set; }

    }
}
