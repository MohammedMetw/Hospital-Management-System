using Hospital.Application.Exceptions;
using Hospital.Application.Features.MedicineInventory.Command;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

public class DeleteMedicineFromInventoryCommandHandler : IRequestHandler<DeleteMedicineFromInventoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMedicineFromInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteMedicineFromInventoryCommand request, CancellationToken cancellationToken)
    {
        var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.MedicineId);
        if (medicine == null)
        {
            throw new NotFoundException("Medicine not found");
        }
        var stockAdjustment = new StockAdjustment
        {
            MedicineInventoryId = medicine.MedicineInventoryId,
            PharmacistId = request.PharmacistId,
            
            QuantityChanged = -medicine.Quantity,
            Reason = request.Reason, 
            AdjustmentDate = DateTime.UtcNow
        };
        await _unitOfWork.StockAdjustments.AddAsync(stockAdjustment);

      
        medicine.Quantity = 0;
        medicine.Status = "Archived"; 
        await _unitOfWork.Medicines.UpdateAsync(medicine);

       
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}