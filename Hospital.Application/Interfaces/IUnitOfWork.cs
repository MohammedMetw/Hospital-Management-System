using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IDepartmentRepository Departments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
