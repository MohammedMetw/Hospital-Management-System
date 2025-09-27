using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
    public class MedicineHistoryDto
    {
        public DateTime EventDate { get; set; }
        public string EventType { get; set; } //  "Stock Added", "Dispensed", "Manual Removal"
        public int QuantityChange { get; set; } // Positive for additions, negative for dispense/removals
        public string PharmacistName { get; set; }
        public string? PatientName { get; set; } 
    }
}
