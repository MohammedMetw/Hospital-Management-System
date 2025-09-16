using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hospital.Application.Features.Pharmacist.Command
{
    public class DeletePharmacistCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
