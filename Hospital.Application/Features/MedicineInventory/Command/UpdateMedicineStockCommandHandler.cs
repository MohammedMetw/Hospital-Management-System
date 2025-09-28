using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Command
{
    public class UpdateMedicineStockCommandHandler : IRequestHandler<UpdateMedicineStockCommand, MedicineInventoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMedicineStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MedicineInventoryDto> Handle(UpdateMedicineStockCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.MedicineId);
            if (medicine == null)
            {
                throw new NotFoundException("Medicine not found");
            }

            int newquantity = request.Quantity - medicine.Quantity;


            medicine.MedicineName = request.MedicineName;
            medicine.Supplier = request.Supplier;
            medicine.Quantity = request.Quantity;
            medicine.UnitPrice = request.UnitPrice;
            medicine.ExpirationDate = request.ExpirationDate;
            medicine.BatchNumber = request.BatchNumber;
            medicine.Status = medicine.Quantity > 0 ? "In Stock" : "Out of Stock";

           

            var stockAdjustment = new StockAdjustment
            {
                MedicineInventoryId = medicine.MedicineInventoryId,
                PharmacistId = request.PharmacistId,
                Reason = request.Reason,
                QuantityChanged = newquantity,
                AdjustmentDate = DateTime.UtcNow
            };
            await _unitOfWork.StockAdjustments.AddAsync(stockAdjustment);

            await _unitOfWork.Medicines.UpdateAsync(medicine);
            await _unitOfWork.SaveChangesAsync();

            return new MedicineInventoryDto
            {
                MedicineInventoryId = medicine.MedicineInventoryId,
                MedicineName = medicine.MedicineName,
                Supplier = medicine.Supplier,
                Quantity = medicine.Quantity,
                UnitPrice = medicine.UnitPrice,
                ExpirationDate = medicine.ExpirationDate,
                BatchNumber = medicine.BatchNumber

            };



        }
    }
}
