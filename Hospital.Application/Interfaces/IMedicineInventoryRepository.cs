using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.Interfaces
{
    public interface IMedicineInventoryRepository : IGenericRepository<MedicineInventory>
    {
       
        Task<IEnumerable< MedicineInventory>> GetMedicinesByNameAsync(string medicineName);

        Task<IEnumerable<MedicineInventory>> GetLowStockMedicinesAsync(int lowStockThreshold);

        Task<IEnumerable<MedicineInventory>> GetExpiredMedicinesAsync();

        Task<MedicineInventory?> GetByQrCodeDataAsync(string qrCodeData);
    }
}
