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
            await _unitOfWork.SaveChangesAsync();

            return newPrescription.PrescriptionId;
                
        }
    }
}
