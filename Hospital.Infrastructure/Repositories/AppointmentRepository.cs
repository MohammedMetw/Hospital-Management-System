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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }
        public new async Task<IEnumerable<Appointment>> GetAllAsync()
        {
                return await _context.Appointments
                             .Include(a => a.patient)
                                .ThenInclude(p => p.ApplicationUser)
                             .Include(a => a.doctor)
                                .ThenInclude(d => d.ApplicationUser)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
           return await _context.Appointments
                                .Include(a => a.patient)
                                        .ThenInclude(p => p.ApplicationUser)
                                .Include(a => a.doctor)
                                        .ThenInclude(d => d.ApplicationUser)
                                .Where(a => a.PatientId == patientId)
                                .ToListAsync();
        }

        public new async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                                 .Include(a => a.patient)
                                    .ThenInclude(p => p.ApplicationUser) 
                                 .Include(a => a.doctor) 
                                    .ThenInclude(d => d.ApplicationUser) 
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsWithinOneHourAsync()
        {
            var now = DateTime.Now;
            var oneHourLater = now.AddHours(1);

            var result = await _context.Appointments
                .Include(a => a.patient) 
                .Include(a => a.doctor)  
                .Where(a => a.Date > now && a.Date <= oneHourLater)
                .ToListAsync();

            return result;
        }
        public async Task <IEnumerable<Appointment>> GetPatientAppointmentsById(int id)
        {
            return await _context.Appointments
         .Where(a => a.DoctorId == id)
         .Include(a => a.patient) 
             .ThenInclude(p => p.ApplicationUser).ToListAsync();

        }


    }
}
