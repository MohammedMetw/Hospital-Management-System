using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Pharmacist.Queries
{
    public class GetPharmacistByIdQueryHandler : IRequestHandler<GetPharmacistByIdQuery, PharmacistDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPharmacistByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PharmacistDto> Handle(GetPharmacistByIdQuery request, CancellationToken cancellationToken)
        {
            var pharmacists = await _unitOfWork.Pharmacists.GetByIdAsync(request.Id);

            if (pharmacists == null)
            {
                throw new NotFoundException("Pharmacist not found");
            }

           
            return new PharmacistDto
            {
                Id = pharmacists.Id,
                FullName = $"{pharmacists.ApplicationUser.FirstName} {pharmacists.ApplicationUser.LastName}",
                Phone = pharmacists.Phone,
                Shift = pharmacists.Shift,
                Email = pharmacists.Email
            };
        }
    }
}
