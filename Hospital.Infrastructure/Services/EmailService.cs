using Microsoft.Extensions.Options;
using Hospital.Application.Common.Setting;
using Hospital.Application.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


public class EmailService : IEmailService
{
   
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendConfirmationEmailAsync(string email, string userId, string token, string baseUrl)
    {
      
        var encodedToken = UrlEncoder.Default.Encode(token);
        var confirmationLink = $"{baseUrl}/api/Account/confirm-email?userId={userId}&token={encodedToken}";


        using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
        {
            smtp.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
            smtp.EnableSsl = true;

           
            var mail = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = "Confirm your Hospital System Account",
                Body = $"<p>Thank you for registering. Please confirm your email by clicking the link below:</p>" +
                       $"<a href='{confirmationLink}'>Confirm Email</a>",
                IsBodyHtml = true 
            };
            mail.To.Add(email);

           
            await smtp.SendMailAsync(mail);
        }
    }
}