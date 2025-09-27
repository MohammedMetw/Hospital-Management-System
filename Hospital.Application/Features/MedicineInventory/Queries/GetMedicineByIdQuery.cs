using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetMedicineByIdQuery : IRequest<MedicineInventoryDto>
    {
        public int MedicineId { get; set; } 
    }
}
