using Hospital.Application.DTOs;
using Hospital.Application.Features.MedicineInventory.Command;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using MediatR;

public class AddMedicineToInventoryCommandHandler : IRequestHandler<AddMedicineToInventoryCommand, MedicineInventoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddMedicineToInventoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MedicineInventoryDto> Handle(AddMedicineToInventoryCommand request, CancellationToken cancellationToken)
    {
        var qrCodeText = $"Medicine: {request.MedicineName}, Batch: {request.BatchNumber}, Expires: {request.ExpirationDate:yyyy-MM-dd}";
        var newMedicine = new MedicineInventory
        {
            MedicineName = request.MedicineName,
            Supplier = request.Supplier,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            ExpirationDate = request.ExpirationDate,
            BatchNumber = request.BatchNumber,
            Status = request.Quantity > 0 ? "In Stock" : "Out of Stock",
            QRCodeData = qrCodeText

        };
        await _unitOfWork.Medicines.AddAsync(newMedicine);
      

        var stockAdjustment = new StockAdjustment
        {
            MedicineInventory = newMedicine,
            MedicineInventoryId = newMedicine.MedicineInventoryId, 
            PharmacistId = request.PharmacistId, 
            QuantityChanged = request.Quantity, 
            Reason = "New Shipment Added",
            AdjustmentDate = DateTime.UtcNow
        };
        await _unitOfWork.StockAdjustments.AddAsync(stockAdjustment);


        await _unitOfWork.SaveChangesAsync(cancellationToken);
    
        return new MedicineInventoryDto { 
               MedicineInventoryId = newMedicine.MedicineInventoryId,
               MedicineName = newMedicine.MedicineName,
               Supplier = newMedicine.Supplier,
               Quantity = newMedicine.Quantity,
               UnitPrice = newMedicine.UnitPrice,
               ExpirationDate = newMedicine.ExpirationDate,
               BatchNumber = newMedicine.BatchNumber,
               Status = newMedicine.Status,
               QRCodeData = qrCodeText

        };
    }
}