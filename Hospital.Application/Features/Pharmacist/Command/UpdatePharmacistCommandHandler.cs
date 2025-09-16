using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Pharmacist.Command
{
    public class UpdatePharmacistCommandHandler : IRequestHandler<UpdatePharmacistCommand , PharmacistDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePharmacistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PharmacistDto> Handle(UpdatePharmacistCommand request, CancellationToken cancellationToken)
        {
            var pharmacist = await _unitOfWork.Pharmacists.GetByIdAsync(request.Id);
            if (pharmacist == null)
            {
                throw new Exception("Pharmacist not found");
            }

            pharmacist.ApplicationUser.FirstName = request.FirstName;
            pharmacist.ApplicationUser.LastName = request.LastName;
            pharmacist.Phone = request.Phone;
            pharmacist.Email = request.Email;
            pharmacist.Shift = request.Shift;

            await _unitOfWork.Pharmacists.UpdateAsync(pharmacist);
            await _unitOfWork.SaveChangesAsync();

            return new PharmacistDto
            {
                Id = pharmacist.Id,
                FullName = $"{pharmacist.ApplicationUser.FirstName} {pharmacist.ApplicationUser.LastName}",
                Phone = pharmacist.Phone,
                Email = pharmacist.Email,
                Shift = pharmacist.Shift,

            };

        }
    }
}
