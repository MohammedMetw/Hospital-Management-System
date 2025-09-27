using MediatR;
using Hospital.Application.Interfaces;
using Hospital.Application.Exceptions;
using Hospital.Application.Features.Prescription.Command;
using Hospital.Domain.Entities;
using Hospital.Application.DTOs;

public class DispensePrescriptionCommandHandler : IRequestHandler<DispensePrescriptionCommand, BillDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public DispensePrescriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BillDto> Handle(DispensePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var UndispensedMedicines = new List<string>();
        var DispensedMedicines = new List<string>();
        var totlaPrice = 0.0m;
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.PrescriptionId);
        if (prescription == null)
        {
            throw new NotFoundException("Prescription not found.");
        }
        if (prescription.IsSold)
        {
            throw new InvalidOperationException("Prescription has already been sold.");
        }
        // Loop through each medicine on the prescription
        foreach (var prescribedMed in prescription.PrescribedMedicines)
        {
            var inventoryStock = await _unitOfWork.Medicines
                .FindFirstAvailableBatchByNameAsync(prescribedMed.MedicineName, prescribedMed.Quantity);

            if (inventoryStock == null)
            {
                UndispensedMedicines.Add($"Not enough stock for {prescribedMed.MedicineName}.");
            }
            else
            {
                DispensedMedicines.Add($"{prescribedMed.MedicineName} dispensed. Quantity : {prescribedMed.Quantity}");
                totlaPrice += prescribedMed.Quantity * inventoryStock.UnitPrice;

                
                inventoryStock.Quantity -= prescribedMed.Quantity;
                await _unitOfWork.Medicines.UpdateAsync(inventoryStock);

                var log = new DispenseLog
                {
                    MedicineInventoryId = inventoryStock.MedicineInventoryId,
                    PatientId = prescription.PatientId,
                    PharmacistId = request.PharmacistId,
                    QuantityDispensed = prescribedMed.Quantity,
                    DispenseDate = DateTime.UtcNow
                };
                await _unitOfWork.DispenseLogs.AddAsync(log);
            }
        }
        prescription.IsSold = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BillDto
        {
            DispensedMedicines = DispensedMedicines,
            UndispensedMedicines = UndispensedMedicines,
            TotalPrice = totlaPrice
        };
    }
}