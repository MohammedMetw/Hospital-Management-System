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
    public class MedicineInventoryRepository : GenericRepository<MedicineInventory>, IMedicineInventoryRepository
    {
        public MedicineInventoryRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<MedicineInventory>> GetExpiredMedicinesAsync()
        {
            return await _context.MedicineInventories
                .Where(m => m.ExpirationDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicineInventory>> GetLowStockMedicinesAsync(int lowStockThreshold)
        {
            return await _context.MedicineInventories.
                Where(m => m.Quantity <= lowStockThreshold)
                .ToListAsync();
        }

        public async Task<IEnumerable< MedicineInventory>> GetMedicinesByNameAsync(string medicineName)
        {
            return await _context.MedicineInventories.
                Where(m => m.MedicineName.ToLower() == medicineName.ToLower())
                .ToListAsync();
        }

       
    }
}
