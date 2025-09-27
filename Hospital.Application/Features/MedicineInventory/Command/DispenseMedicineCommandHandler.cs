using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

namespace Hospital.Application.Features.MedicineInventory.Command
{
    public class DispenseMedicineCommandHandler : IRequestHandler<DispenseMedicineCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DispenseMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DispenseMedicineCommand request, CancellationToken cancellationToken)
        {
            var medicineStock = await _unitOfWork.Medicines.GetByIdAsync(request.MedicineInventoryId);
            if (medicineStock == null)
            {
                throw new NotFoundException("Medicine stock not found.");
            }
            if (medicineStock.Quantity < request.QuantityToDispense)
            {
                throw new InvalidOperationException("Not enough quantity in stock.");
            }
            // 1. Decrease the stock quantity
            medicineStock.Quantity -= request.QuantityToDispense;
            await _unitOfWork.Medicines.UpdateAsync(medicineStock);

           
            var dispenseLog = new DispenseLog
            {
                MedicineInventoryId = request.MedicineInventoryId,
                PatientId = request.PatientId,
                PharmacistId = request.PharmacistId,
                QuantityDispensed = request.QuantityToDispense,
                DispenseDate = DateTime.UtcNow
            };
            await _unitOfWork.DispenseLogs.AddAsync(dispenseLog);

            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
