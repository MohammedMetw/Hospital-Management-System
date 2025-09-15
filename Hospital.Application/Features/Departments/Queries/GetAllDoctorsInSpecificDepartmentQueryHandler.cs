using Hospital.Application.Interfaces;
using Hospital.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Queries
{
    public class GetAllDoctorsInSpecificDepartmentQueryHandler : IRequestHandler<GetAllDoctorsInSpecificDepartmentQuery,IEnumerable<DoctorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDoctorsInSpecificDepartmentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorsInSpecificDepartmentQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _unitOfWork.Departments.GetAllDoctorsInSpecificDepartment(request.DepartmentId);


            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName =$"{d.ApplicationUser.FirstName } {d.ApplicationUser.LastName}" ,
                Specialty = d.Specialty,
                DepartmentId = d.DepartmentId
            }).ToList();
        }

    }
}
