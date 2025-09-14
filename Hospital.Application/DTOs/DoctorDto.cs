using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
    }
}
