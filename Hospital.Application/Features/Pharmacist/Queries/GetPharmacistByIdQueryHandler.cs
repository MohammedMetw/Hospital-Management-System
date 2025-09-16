using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
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
            var pharmacists = await _unitOfWork.Pharmacists.GetByIdAsync(request.id);

            if (pharmacists == null)
            {
                throw new Exception("Pharmacist not found");
            }

           
            return new PharmacistDto
            {
                Id = pharmacists.Id,
                Phone = pharmacists.Phone,
                Shift = pharmacists.Shift,
                Email = pharmacists.Email
            };
        }
    }
}
