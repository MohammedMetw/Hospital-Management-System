using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;

namespace Hospital.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            
            var departments = await _unitOfWork.Departments.GetAllAsync();

            //var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments); for future 
            var departmentDtos = departments.Select(dept => new DepartmentDto
            {
                Id = dept.Id,
                Name = dept.Name
            }).ToList();

            
            return departmentDtos;
        }
    }
}
