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
   public class AccountantRepository: GenericRepository<Accountant>, IAccountantRepository
    {
        public AccountantRepository(AppDbContext context) : base(context)
        {

        }
        public new async Task<IEnumerable<Accountant>> GetAllAsync()
        {
            return await _context.Accountants
                                 .Include(n => n.ApplicationUser)
                                  .ToListAsync();
        }

        public new async Task<Accountant?> GetByIdAsync(int id)
        {
            return await _context.Accountants
                                 .Include(n => n.ApplicationUser)
                                 .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
