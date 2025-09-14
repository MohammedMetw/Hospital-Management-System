using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Command
{
   public class UpdateDepartmentCommandHandler:IRequestHandler<UpdateDepartmentCommand,DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }
            department.Name = request.Name;
            await _unitOfWork.Departments.UpdateAsync(department);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };
        }

    }
}
