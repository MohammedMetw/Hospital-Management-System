using Hospital.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Command
{
    public class DeleteDepartmentCommand:IRequest<DepartmentDto>
    {
        public int Id { get; set; }

    }
}
