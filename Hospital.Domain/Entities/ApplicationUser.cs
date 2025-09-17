using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public Doctor? DoctorProfile { get; set; }
        public virtual Patient? PatientProfile { get; set; }
        public virtual Nurse? NurseProfile { get; set; }
        public virtual Pharmacist? PharmacistProfile { get; set; }
        public virtual Accountant? AccountantProfile { get; set; }
    }
}
