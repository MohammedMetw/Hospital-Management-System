using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Persistence;

namespace Hospital.Infrastructure.Repositories
{
    public class StockAdjustmentRepository : GenericRepository<StockAdjustment>, IStockAdjustmentRepository
    {
        public StockAdjustmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
