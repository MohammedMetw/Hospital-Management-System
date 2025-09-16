using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Nurse.Command
{
  public  class UpdateNurseCommand : IRequest<NurseDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Shift { get; set; }
        public int DepartmentId { get; set; }


        
    }
}
