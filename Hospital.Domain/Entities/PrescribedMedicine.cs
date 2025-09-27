using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
   
    public class PrescribedMedicine
    {
        public int PrescribedMedicineId { get; set; }
        public int PrescriptionId { get; set; } 
        public virtual Prescription Prescription { get; set; }
        public string MedicineName { get; set; } 
        
        public string Instructions { get; set; } 
        public int Quantity { get; set; }
    }
}
