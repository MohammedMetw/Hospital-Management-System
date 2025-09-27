using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.Repositories
{
    public class PrescribedMedicineRepository : GenericRepository<PrescribedMedicine>, IPrescribedMedicineRepository
    {
        public PrescribedMedicineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
