using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.DTOs;
using MediatR;

namespace Hospital.Application.Features.Patient.Command
{
    public class UpdatePatientCommand : IRequest<PatientsDto>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
       
        public required string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public required string City { get; set; }

        public required string Gender { get; set; }
    }
}
