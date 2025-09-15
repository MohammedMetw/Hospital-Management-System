using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public required string Specialty { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }





        // 1. Foreign Key property. the ID of the user.
        // [ForeignKey("ApplicationUser")]
        public required string ApplicationUserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }



        //  foreign key property for Department
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
