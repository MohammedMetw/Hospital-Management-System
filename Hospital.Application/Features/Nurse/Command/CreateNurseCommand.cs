using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Nurse.Command
{
   public class CreateNurseCommand : IRequest<NurseDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public String PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Shift { get; set; }
        public int DepartmentId { get; set; }

    }
}
