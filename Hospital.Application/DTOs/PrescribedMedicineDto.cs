using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.DTOs
{
    public class PrescribedMedicineDto
    {
      
        public string MedicineName { get; set; }
        public string Instructions { get; set; }
        public int Quantity { get; set; }
    }
}
