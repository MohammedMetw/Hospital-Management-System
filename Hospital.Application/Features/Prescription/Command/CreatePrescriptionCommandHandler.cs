using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Prescription.Command
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePrescriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            if (appointment.Status == "Completed" || appointment.Status == "Cancelled")
            {
                throw new Exception("This appointment has already been completed or cancelled.");
            }
            if(appointment.PatientId != request.PatientId || appointment.DoctorId != request.DoctorId)
            {
                throw new Exception("This appointment is not for this patient or doctor");
            }
            var newPrescription = new Domain.Entities.Prescription()
            {
                PrescriptionDate = DateTime.Now,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId

            };
          await  _unitOfWork.Prescriptions.AddAsync(newPrescription);
            
            foreach (var medicinesDto in request.PrescribedMedicines)
            {
                
                var newPrescribedMedicine = new Domain.Entities.PrescribedMedicine
                {
                    
                    Prescription = newPrescription, 
                    MedicineName = medicinesDto.MedicineName,
                    Instructions = medicinesDto.Instructions,
                    Quantity = medicinesDto.Quantity
                    
                };
               
                await _unitOfWork.PrescribedMedicines.AddAsync(newPrescribedMedicine);
            }
            appointment.Status = "Completed";
            await _unitOfWork.SaveChangesAsync();

            return newPrescription.PrescriptionId;
                
        }
    }
}
