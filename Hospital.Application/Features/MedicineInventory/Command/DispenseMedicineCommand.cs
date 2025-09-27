using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Command
{
    public class DispenseMedicineCommand : IRequest<Unit>
    {
        public int PharmacistId { get; set; }
        public int PatientId { get; set; }
        public int MedicineInventoryId { get; set; } 
        public int QuantityToDispense { get; set; }
    }
}
