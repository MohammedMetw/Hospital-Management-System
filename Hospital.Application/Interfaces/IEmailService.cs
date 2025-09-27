using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string userId, string token, string baseUrl);
    }
}
