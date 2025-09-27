using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }


        public int DoctorId { get; set; }
        public virtual Doctor doctor { get; set; }

        public int? NurseId { get; set; }
        public virtual Nurse? Nurse { get; set; }
        public int PatientId { get; set; }
        public virtual Patient patient { get; set; }
    }
}
