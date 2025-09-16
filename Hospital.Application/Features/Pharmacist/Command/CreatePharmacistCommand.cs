using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Pharmacist.Command
{
    public class CreatePharmacistCommand : IRequest<int>
    {
        // User Account Info
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        // Pharmacit Profile Info
        public required string Shift { get; set; }
        public required string Phone { get; set; }

       
    }
}
