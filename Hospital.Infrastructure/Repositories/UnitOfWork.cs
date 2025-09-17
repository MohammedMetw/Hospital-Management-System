using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IDoctorRepository Doctors { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public INurseRepository Nurses { get; private set; }

        public IPharmacistRepository Pharmacists { get; private set; }
        public IAccountantRepository Accountants { get; private set; }



        public UnitOfWork(
        AppDbContext context,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IDepartmentRepository departmentRepository,
        INurseRepository nurseRepository,
        IPharmacistRepository pharmacistRepository,
        IAccountantRepository accountantRepository)
        {
            _context = context;


            Doctors = doctorRepository;
            Patients = patientRepository;
            Departments = departmentRepository;
            Nurses = nurseRepository;
            Pharmacists = pharmacistRepository;
            Accountants = accountantRepository;
        }



        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
