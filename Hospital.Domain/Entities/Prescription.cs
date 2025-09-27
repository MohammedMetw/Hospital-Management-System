using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime PrescriptionDate { get; set; }

        public bool IsSold { get; set; } = false; // to prevent Idempotency Error

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

       
        public virtual ICollection<PrescribedMedicine> PrescribedMedicines { get; set; } = new List<PrescribedMedicine>();
    }
}
