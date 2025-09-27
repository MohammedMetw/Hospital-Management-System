using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
    public class MedicineInventoryDto
    {
        public int MedicineInventoryId { get; set; }

       // public int PharmacistId { get; set; }

        public string MedicineName { get; set; }

        public string Supplier { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string BatchNumber { get; set; }

        public string Status { get; set; } // "In Stock," "Low Stock," "Expired"

        public string QRCodeData { get; set; }

       
    }
}
