using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Patient.Queries
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPatientsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PatientsDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();

            var pateintsDto = patients.Select(x => new PatientsDto
            {
                Id = x.Id,
                FullName = $"{x.ApplicationUser.FirstName} {x.ApplicationUser.LastName}",
                Email = x.Email,
                Phone = x.Phone,
                Gender = x.Gender,
                City = x.City,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth
                
            });

            return pateintsDto;


        }
    }
}
