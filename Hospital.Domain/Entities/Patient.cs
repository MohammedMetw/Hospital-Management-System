using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address{ get; set; }
        public required string City { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }

        public required string Gender { get; set; } 



        // 1. Foreign Key property. the ID of the user.
        // [ForeignKey("ApplicationUser")]
        public  required string ApplicationUserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }


        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
