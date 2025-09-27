using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Command
{
    public class UpdateMedicineStockCommand : IRequest<MedicineInventoryDto>
    {
        public int MedicineId { get; set; }
        public int PharmacistId { get; set; }
        public string MedicineName { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string BatchNumber { get; set; }
        public string Reason { get; set; }


    }
}
