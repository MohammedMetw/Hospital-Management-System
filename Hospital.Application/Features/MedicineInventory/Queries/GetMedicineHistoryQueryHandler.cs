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
    public class GetMedicineHistoryQueryHandler : IRequestHandler<GetMedicineHistoryQuery, IEnumerable<MedicineHistoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MedicineHistoryDto>> Handle(GetMedicineHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = new List<MedicineHistoryDto>();

            // all stock adjustments for this medicine
            var adjustments = await _unitOfWork.StockAdjustments
                .FindAllAsync(a => a.MedicineInventoryId == request.MedicineInventoryId, includes: new[] { "Pharmacist.ApplicationUser" });

            foreach (var adj in adjustments)
            {
                history.Add(new MedicineHistoryDto
                {
                    EventDate = adj.AdjustmentDate,
                    EventType = adj.Reason,
                    QuantityChange = adj.QuantityChanged,
                    PharmacistName = $"{adj.Pharmacist.ApplicationUser.FirstName} {adj.Pharmacist.ApplicationUser.LastName}",
                    PatientName = null 
                });
            }

            //  all dispense logs for this medicine
            var dispenses = await _unitOfWork.DispenseLogs
                .FindAllAsync(d => d.MedicineInventoryId == request.MedicineInventoryId, includes: new[] { "Pharmacist.ApplicationUser", "Patient.ApplicationUser" });

            foreach (var log in dispenses)
            {
                history.Add(new MedicineHistoryDto
                {
                    EventDate = log.DispenseDate,
                    EventType = "Dispensed to Patient",
                    QuantityChange = -log.QuantityDispensed, 
                    PharmacistName = $"{log.Pharmacist.ApplicationUser.FirstName} {log.Pharmacist.ApplicationUser.LastName}",
                    PatientName = $"{log.Patient.ApplicationUser.FirstName} {log.Patient.ApplicationUser.LastName}"
                });
            }

            
            return history.OrderByDescending(h => h.EventDate);
        }
    }
}
