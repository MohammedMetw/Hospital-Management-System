using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
   public class Nurse
    {
        public int Id { get; set; }
        public  string Phone { get; set; }
        public  string Email { get; set; }
        public  string Shift { get; set; }  // Morning, Evening, Night

        public  string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public  int DepartmentId { get; set; }
        public Department Department { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
