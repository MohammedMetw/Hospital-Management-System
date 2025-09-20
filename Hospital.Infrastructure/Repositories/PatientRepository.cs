using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }
        public new async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                                 .Include(d => d.ApplicationUser)
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Patient?> GetByUserIdAsync(string userId)
        {
            return await _context.Patients.FirstOrDefaultAsync(d => d.ApplicationUserId == userId);
        }

        public async Task<Patient?> GetPatientByName(string name)
        {
            return await _context.Patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => (p.ApplicationUser.FirstName + " " + p.ApplicationUser.LastName) == name);
        }
    }
}