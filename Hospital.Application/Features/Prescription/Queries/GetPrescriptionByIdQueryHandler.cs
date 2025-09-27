using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Prescription.Queries
{
    public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPrescriptionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PrescriptionDto> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(request.PrescriptionId);
            if (prescription == null)
            {
                throw new Exception("Prescription not found");
            }
            return new PrescriptionDto
            {
                PrescriptionId = request.PrescriptionId,
                PatientName = $"{prescription.Patient.ApplicationUser.FirstName} {prescription.Patient.ApplicationUser.LastName}",
                PatientId = prescription.PatientId,
                DoctorName = $"{prescription.Doctor.ApplicationUser.FirstName} {prescription.Doctor.ApplicationUser.LastName}",
                DoctorId = prescription.DoctorId,

                PrescribedMedicines = prescription.PrescribedMedicines.Select(m => new PrescribedMedicineDto
                {
                    MedicineName = m.MedicineName,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions



                }).ToList()


            };
        }
    }
}
