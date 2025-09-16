using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories
{
    public class PharmacistRepository : GenericRepository<Pharmacist> , IPharmacistRepository
    {

        public PharmacistRepository(AppDbContext context) : base(context)
        {
        }


        public new async Task<IEnumerable<Pharmacist>> GetAllAsync()
        {
            return await _context.pharmacists.
                Include(p => p.ApplicationUser).
                ToListAsync();
        }

        public new async Task<Pharmacist?> GetByIdAsync(int id)
        {
            return await _context.pharmacists.
                Include(p => p.ApplicationUser).
                FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
