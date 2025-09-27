using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetExpiredMedicinesQueryHandler : IRequestHandler<GetExpiredMedicinesQuery,IEnumerable<MedicineInventoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExpiredMedicinesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MedicineInventoryDto>> Handle(GetExpiredMedicinesQuery request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.GetExpiredMedicinesAsync();
            return medicines.Select(x => new MedicineInventoryDto
            {
                MedicineInventoryId = x.MedicineInventoryId,
                MedicineName = x.MedicineName,
                Supplier = x.Supplier,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                ExpirationDate = x.ExpirationDate,
                BatchNumber = x.BatchNumber,
                Status = x.Status,

            });
        }
    }
}
