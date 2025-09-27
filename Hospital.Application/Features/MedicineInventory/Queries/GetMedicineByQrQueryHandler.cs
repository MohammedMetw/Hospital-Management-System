using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Queries
{
    public class GetMedicineByQrQueryHandler : IRequestHandler<GetMedicineByQrQuery, MedicineInventoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineByQrQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicineInventoryDto> Handle(GetMedicineByQrQuery request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByQrCodeDataAsync(request.QrCodeData);
            if (medicine == null)
            {
                throw new NotFoundException("Medicine with the provided QR code was not found.");
            }
            if(medicine.Quantity == 0)
            {
                throw new NotFoundException("Medicine with the provided QR code is out of stock.");
            }
            return new MedicineInventoryDto 
            { 
                MedicineName = medicine.MedicineName,
                UnitPrice = medicine.UnitPrice,
                Status = medicine.Status,
                ExpirationDate= medicine.ExpirationDate
             
            };
        }
    }
}
