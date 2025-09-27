using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Command
{
    public class DeleteMedicineFromInventoryCommand : IRequest<Unit>
    {
        public int MedicineId { get; set; }
        public int PharmacistId { get; set; } 
        public string Reason { get; set; } 
    }
}
