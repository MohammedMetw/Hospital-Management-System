using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Features.Patient.Queries;
using Hospital.Application.Interfaces;
using MediatR;

public class PatientHistoryQueryHandler : IRequestHandler<PatientHistoryQuery, PatientHistoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public PatientHistoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientHistoryDto> Handle(PatientHistoryQuery request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetPatientHistoryAsync(request.PatientId);
        if (patient == null)
        {
            throw new NotFoundException("Patient not found");
        }

        var historyDto = new PatientHistoryDto
        {
            PatientId = patient.Id,
            PatientName = $"{patient.ApplicationUser.FirstName} {patient.ApplicationUser.LastName}",

            Appointments = patient.Appointments.Select(a => new AppointmentHistoryDto
            {
                Id = a.Id,
                DoctorName= $"{a.doctor.ApplicationUser.FirstName} {a.doctor.ApplicationUser.LastName}",
                Status = a.Status,
                AppointmentDateTime = a.Date
            }).ToList(),

            Prescriptions = patient.Prescriptions.Select(p => new PrescriptionHistoryDto
            {
                Id = p.PrescriptionId,
                PrescriptionDate = p.PrescriptionDate,
                Medicines = p.PrescribedMedicines.Select(m => m.MedicineName).ToList()
            }).ToList(),

           
            PharmacyBills = patient.DispenseLogs.Select(dl => new BillHistoryDto
            {
                DispenseLogId = dl.Id,
                DispenseDate = dl.DispenseDate,
                MedicineName = dl.MedicineInventory.MedicineName,
                Quantity = dl.QuantityDispensed,
                
                TotalPrice = dl.QuantityDispensed * dl.MedicineInventory.UnitPrice,
                PharmacistName = $"{dl.Pharmacist.ApplicationUser.FirstName} {dl.Pharmacist.ApplicationUser.LastName}"
            }).ToList()
        };

        return historyDto;
    }
}