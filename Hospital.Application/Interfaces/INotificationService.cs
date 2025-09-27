using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface INotificationService
    {
        
        Task SendLowStockAlertAsync(string medicineName, int currentQuantity);
    }
}
