using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Application.Interfaces
{
    public interface IStockAdjustmentRepository : IGenericRepository<StockAdjustment>
    {
        // You can add specific methods for stock adjustments here in the future if needed.
    }
}
