using HotelBackend.Identity.Common.Interfaces;
using HotelBackend.Identity.Common.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace HotelBackend.Identity.Services
{
    public class EmailSender
        : IEmailSender
    {
        private readonly EmailSenderSettings _emailSenderSettings;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(
            IOptionsMonitor<EmailSenderSettings> emailSernderSettings,
            ILogger<EmailSender> logger)
        {
            _emailSenderSettings = emailSernderSettings.CurrentValue ?? throw new ArgumentNullException(nameof(emailSernderSettings));
            _logger = logger;
        }

        public async Task SendAsync(
            string to,
            string subject,
            string body,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Отправка email на: {to}", to);

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(
                        _emailSenderSettings.From,
                        _emailSenderSettings.DisplayName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                using var smtpClient = new SmtpClient(
                    _emailSenderSettings.SmtpServer,
                    _emailSenderSettings.Port)
                {
                    Credentials = new NetworkCredential(
                        _emailSenderSettings.UserName,
                        _emailSenderSettings.AppPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);

                _logger.LogInformation($"Email отправлен: {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка отправки email на {to}");
                throw; 
            }
        }
    }
}
