using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hospital.Application.Common.Setting;
using Hospital.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Hospital.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly EmailSettings _emailSettings;

        public NotificationService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendLowStockAlertAsync(string medicineName, int currentQuantity)
        {
            
            var managerEmail = "esraabakkar959@gmail.com";

            using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                smtp.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = $"[LOW STOCK ALERT] - {medicineName}",
                    Body = $"<h1>Low Stock Notification</h1>" +
                           $"<p>This is an automated alert to inform you that the stock for the medicine " +
                           $"<strong>{medicineName}</strong> is running low.</p>" +
                           $"<p><strong>Current Quantity:</strong> {currentQuantity}</p>" +
                           $"<p>Please take the necessary action to reorder this item.</p>",
                    IsBodyHtml = true
                };
                mail.To.Add(managerEmail);

                await smtp.SendMailAsync(mail);
            }
        }
    }
}