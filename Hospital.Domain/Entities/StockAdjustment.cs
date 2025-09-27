using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class StockAdjustment
    {
        public int Id { get; set; }
        public int MedicineInventoryId { get; set; }
        public virtual MedicineInventory MedicineInventory { get; set; }

        public int PharmacistId { get; set; } 
        public virtual Pharmacist Pharmacist { get; set; }

        public int QuantityChanged { get; set; }
        public string Reason { get; set; } // ("Damaged", "Expired", "Restock")
        public DateTime AdjustmentDate { get; set; }
    }
}
