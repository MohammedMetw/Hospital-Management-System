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
    public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, MedicineInventoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMedicineByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MedicineInventoryDto> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken)
        {
            var getmedicine = await _unitOfWork.Medicines.GetByIdAsync(request.MedicineId);

            var mdeicine = new MedicineInventoryDto
            {
                MedicineInventoryId = getmedicine.MedicineInventoryId,
                MedicineName = getmedicine.MedicineName,
                Supplier = getmedicine.Supplier,
                Quantity = getmedicine.Quantity,
                UnitPrice = getmedicine.UnitPrice,
                ExpirationDate = getmedicine.ExpirationDate,
                BatchNumber = getmedicine.BatchNumber,
                Status = getmedicine.Status

            };
            return mdeicine;
        }
    }
}
