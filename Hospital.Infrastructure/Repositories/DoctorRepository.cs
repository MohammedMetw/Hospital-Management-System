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
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

        // for include
        public new async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                                 .Include(d => d.ApplicationUser)
                                 .Include(d => d.Department)
                                 .ToListAsync();
        }
        public new async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors
                                 .Include(d => d.ApplicationUser)
                                 .Include(d => d.Department)
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor?> GetByUserIdAsync(string userId)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.ApplicationUserId == userId);
        }

    }
}
