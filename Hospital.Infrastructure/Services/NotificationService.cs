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

        public async Task ChangePasswordAlertAsync(string email)
        {
            using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                smtp.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = "[PASSWORD CHANGED SUCCESSFULLY]",
                    Body = $"<h1>Password Changed Successfully</h1>" +
                           $"<p>Hello,</p>" +
                           $"<p>This is to notify you that your account password has been <strong>changed successfully</strong>.</p>" +
                           $"<p>If you made this change, no further action is required.</p>" +
                           $"<p>If you did <strong>not</strong> request this change, please reset your password immediately and contact our support team.</p>" +
                           $"<br/>" +
                           $"<p>Thank you,</p>" +
                           $"<p><em>Your Security Team</em></p>",

                    IsBodyHtml = true
                };
                mail.To.Add(email);

                await smtp.SendMailAsync(mail);
            }
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
        public async Task SendreminderBeforeAppiontmentByHour(string email)
        {
            string message = "There exist an hour before yourAppointment";
        
            using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                smtp.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = "[Appointment Reminder]",
                    Body = message,
                    IsBodyHtml = true 
                };
                mail.To.Add(email);

                await smtp.SendMailAsync(mail);
            }
        }





    }

}
