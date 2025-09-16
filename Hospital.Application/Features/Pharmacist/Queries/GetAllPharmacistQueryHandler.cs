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
    public class GetAllPharmacistQueryHandler : IRequestHandler<GetAllPharmacistQuery, IEnumerable<PharmacistDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPharmacistQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PharmacistDto>> Handle(GetAllPharmacistQuery request, CancellationToken cancellationToken)
        {
           var pharmacists = await _unitOfWork.Pharmacists.GetAllAsync();

            var pharmacistsDto = pharmacists.Select(p => new PharmacistDto
            {
                Id = p.Id,
                FullName = $"{p.ApplicationUser.FirstName} {p.ApplicationUser.LastName}",
                Email = p.Email,
                Phone = p.Phone,
                Shift = p.Shift

            }).ToList();

           return pharmacistsDto;
        }
    }
}
