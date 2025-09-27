using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class DispenseLog
    {
        public int Id { get; set; }
        public int MedicineInventoryId { get; set; } 
        public virtual MedicineInventory MedicineInventory { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int PharmacistId { get; set; } 
        public virtual Pharmacist Pharmacist { get; set; }

        public int QuantityDispensed { get; set; }
        public DateTime DispenseDate { get; set; }
    }
}
