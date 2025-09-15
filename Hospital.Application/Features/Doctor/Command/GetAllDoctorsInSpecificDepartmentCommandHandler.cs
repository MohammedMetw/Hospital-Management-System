using Hospital.Application.Interfaces;
using Hospital.Application.DTOs; 
using Hospital.Application.Features.Doctor.Command; 
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Application.Features.Departments.Command;


namespace Hospital.Application.Features.Doctor.Command
{
    public class GetAllDoctorsInSpecificDepartmentCommandHandler : IRequestHandler<GetAllDoctorsInSpecificDepartmentCommand,IEnumerable<DoctorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDoctorsInSpecificDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetAllDoctorsInSpecificDepartmentCommand request, CancellationToken cancellationToken)
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
