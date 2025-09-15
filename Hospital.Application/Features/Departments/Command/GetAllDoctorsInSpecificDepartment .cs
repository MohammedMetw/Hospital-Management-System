using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Departments.Command
{
    public class GetAllDoctorsInSpecificDepartmentCommand : IRequest<IEnumerable<DoctorDto>>
    {
        public int DepartmentId { get; set; }
        public GetAllDoctorsInSpecificDepartmentCommand(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
