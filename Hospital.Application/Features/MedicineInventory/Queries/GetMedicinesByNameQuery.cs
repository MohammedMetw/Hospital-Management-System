using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetMedicinesByNameQuery : IRequest<IEnumerable< MedicineInventoryDto>>
    {
        public string MedicineName { get; set; }
    }
}
