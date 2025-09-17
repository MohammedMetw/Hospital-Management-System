using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
      
        public PatientRepository(AppDbContext context) { }

        public Task AddAsync(Patient entity) => throw new NotImplementedException();
        public Task DeleteAsync(Patient entity) => throw new NotImplementedException();
        public Task<IEnumerable<Patient>> GetAllAsync() => throw new NotImplementedException();
        public Task<Patient?> GetByIdAsync(int id) => throw new NotImplementedException();
        public Task<Patient?> GetPatientByName(string name) => throw new NotImplementedException();
        public Task UpdateAsync(Patient entity) => throw new NotImplementedException();
        
        Task<int> IGenericRepository<Patient>.CountAsync()
        {
            throw new NotImplementedException();
        }
    }
}