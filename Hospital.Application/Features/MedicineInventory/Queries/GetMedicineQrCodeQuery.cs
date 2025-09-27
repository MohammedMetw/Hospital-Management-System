using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetMedicineQrCodeQuery : IRequest<byte[]>
    {
        public int MedicineId { get; set; }
    }
}
