using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Pharmacist>  pharmacists { get; set; }
        public DbSet<Accountant> Accountants { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            

            // One-to-One: ApplicationUser <-> Doctor
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.DoctorProfile)
                .WithOne(d => d.ApplicationUser)
                .HasForeignKey<Doctor>(d => d.ApplicationUserId);

            // One-to-One: ApplicationUser <-> Patient
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.PatientProfile)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Patient>(p => p.ApplicationUserId);

            // One-to-One: ApplicationUser <-> Nurse
            builder.Entity<ApplicationUser>().
                HasOne(u=>u.NurseProfile)
                .WithOne(n => n.ApplicationUser)
                .HasForeignKey<Nurse>(n => n.ApplicationUserId);

            // One-to-One: ApplicationUser <-> Pharmacist
            builder.Entity<ApplicationUser>().
                HasOne(u => u.PharmacistProfile).
                WithOne(p => p.ApplicationUser)
                .HasForeignKey<Pharmacist>(p => p.ApplicationUserId);
            // One-to-One: ApplicationUser <-> Accountant
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.AccountantProfile)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Accountant>(a => a.ApplicationUserId);



        }
    }

}
