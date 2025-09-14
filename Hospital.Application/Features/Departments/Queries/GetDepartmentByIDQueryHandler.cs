using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Queries
{
    public class GetDepartmentByIDQueryHandler : IRequestHandler<GetDepartmentByIDQuery, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDepartmentByIDQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIDQuery request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.Id);

            if (department == null)
                return null; 

            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return departmentDto;
        }
    }
}


