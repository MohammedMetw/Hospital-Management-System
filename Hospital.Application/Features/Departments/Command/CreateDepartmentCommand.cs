using Hospital.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Command
{
   public class CreateDepartmentCommand :IRequest<DepartmentDto>
    {
        public required string Name { get; set; }
    }
}
