using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Features.Departments.Queries;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Nurse.Queries
{
  public  class GetAllNuressQueryHandler : IRequestHandler<GetAllNuresQuery,IEnumerable<NurseDto>>
    {
        
            private readonly IUnitOfWork _unitOfWork;

            public GetAllNuressQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
        public async Task<IEnumerable<NurseDto>>Handle(GetAllNuresQuery request , CancellationToken cancellationToken)
        {
            var nurses = await _unitOfWork.Nurses.GetAllAsync();
            var nurseDtos = nurses.Select(nurse => new NurseDto
            {
                Id = nurse.Id,
                FullName = $"{nurse.ApplicationUser.FirstName} {nurse.ApplicationUser.LastName}",
               
                Email = nurse.ApplicationUser.Email,
                PhoneNumber = nurse.ApplicationUser.PhoneNumber,
                Shift = nurse.Shift,
                DepartmentId = nurse.DepartmentId,
               
            }).ToList();
            return nurseDtos;

        }
          

        }
}
