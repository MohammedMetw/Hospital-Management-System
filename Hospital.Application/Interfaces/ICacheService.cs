using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
   public interface ICacheService
    {
        Task BlacklistTokenAsync(string token);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }
}
