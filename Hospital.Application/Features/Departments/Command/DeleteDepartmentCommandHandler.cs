using Hospital.Application.DTOs;
using Hospital.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Command
{
   public class DeleteDepartmentCommandHandler:IRequestHandler<DeleteDepartmentCommand, DepartmentDto> 
    {
        readonly IUnitOfWork _unitOfWork;
        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DepartmentDto> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }

            await _unitOfWork.Departments.DeleteAsync(department);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };


        }
    }
}
