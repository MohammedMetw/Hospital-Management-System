using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }
     
        Task<List<Doctor>> IDepartmentRepository.GetAllDoctorsInSpecificDepartment(int departmentId)
        {
            var doctors = _context.Patient
              .Include(d => d.ApplicationUser)
              .Where(d => d.DepartmentId == departmentId)
              .ToListAsync();
            return doctors;
        }
    }
}

