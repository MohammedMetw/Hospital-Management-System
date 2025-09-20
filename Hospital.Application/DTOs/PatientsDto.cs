using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
    public class PatientsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public  string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string City { get; set; }
        
        public string? Address { get; set; }
     
    }
}
