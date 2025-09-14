using Hospital.Application.DTOs;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Features.Departments.Queries
{
    public class GetDepartmentByIDQuery : IRequest<DepartmentDto>
    {
        public int Id { get; set; }

        public GetDepartmentByIDQuery(int id)
        {
            Id = id;
        }
    }

}
