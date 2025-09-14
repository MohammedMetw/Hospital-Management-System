using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Doctor.Command
{
    public class CreateDoctorCommand : IRequest<int>
    {
        // User Account Info
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        // Doctor Profile Info
        public required string Specialty { get; set; }
        public required string Phone { get; set; }

        // Link to Department
        public int DepartmentId { get; set; }
    }
}
