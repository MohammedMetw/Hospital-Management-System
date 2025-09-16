using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
     public class Pharmacist
    {
        public int Id { get; set; }
        public required string Phone { get; set; }

        public string Shift { get; set; }
        public required string Email { get; set; }





        // 1. Foreign Key property. the ID of the user.
        // [ForeignKey("ApplicationUser")]
        public required string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
