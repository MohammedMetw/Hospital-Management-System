using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
   public class NurseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Shift { get; set; }
        public int DepartmentId { get; set; }


    }
}
