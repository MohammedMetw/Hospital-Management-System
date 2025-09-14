using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Queries
{
    public class GetDepartmentByIDQueryHandler : IRequestHandler<GetDepartmentByIDQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentByIDQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIDQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);

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


