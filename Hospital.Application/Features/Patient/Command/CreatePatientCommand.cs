using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Patient.Command
{
    public class CreatePatientCommand : IRequest<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public required string City { get; set; }

        public required string Gender { get; set; }
    }
}
