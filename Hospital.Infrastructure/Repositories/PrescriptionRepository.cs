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
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Prescription?> GetByIdAsync(int id)
        {
            return await _context.Prescriptions
            .Include(p => p.Patient)
                .ThenInclude(patient => patient.ApplicationUser)
            .Include(p => p.Doctor)
                .ThenInclude(doctor => doctor.ApplicationUser)
            .Include(p => p.PrescribedMedicines)
            .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }
    }
}
