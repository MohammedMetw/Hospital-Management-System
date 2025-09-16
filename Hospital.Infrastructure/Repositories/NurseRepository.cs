using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;
using Hospital.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Hospital.Infrastructure.Repositories
{
    public class NurseRepository : GenericRepository<Nurse>, INurseRepository
    {
        

        public NurseRepository(AppDbContext context) : base(context)
        {
           
        }

        public new async Task<IEnumerable<Nurse>> GetAllAsync()
        {
            return await _context.Nurses
                                 .Include(n => n.ApplicationUser)
                                 .Include(n => n.Department)
                                 .ToListAsync();
        }

        public new async Task<Nurse?> GetByIdAsync(int id)
        {
            return await _context.Nurses
                                 .Include(n => n.ApplicationUser)
                                 .Include(n => n.Department)
                                 .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
