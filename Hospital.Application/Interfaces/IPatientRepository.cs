using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetPatientByName(string name);

        Task<Patient?> GetPatientHistoryAsync(int id);
    }


}
