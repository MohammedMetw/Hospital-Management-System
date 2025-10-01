using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Application.Interfaces;

namespace Hospital.Infrastructure.BackgroundServices
{
    public class AppointmentReminderBackgroundService : BackgroundService
    {
        private readonly IEmailService _emailService;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);

        public AppointmentReminderBackgroundService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine($"⏰ [Reminder Service] Running at {DateTime.Now}");

                    
                    await _emailService.SendRemindersForUpcomingAppointmentsAsync();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"❌ Error sending reminders: {ex.Message}");
                }

                
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
