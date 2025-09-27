using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.DTOs
{
    public class BillDto
    {
            public ICollection<string> DispensedMedicines { get; set; }

            public ICollection<string> UndispensedMedicines { get; set; }

            public decimal TotalPrice { get; set; }
    } 
}
