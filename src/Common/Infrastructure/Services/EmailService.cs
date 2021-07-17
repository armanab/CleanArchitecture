using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace CleanApplication.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger=logger;
        }
        public  bool SendEmail(EmailApp email)
        {

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.From);
            mailMessage.To.Add(new MailAddress(email.To));

            mailMessage.Subject = email.Subject;
            mailMessage.IsBodyHtml = email.IsBodyHtml;
            mailMessage.Body = email.Body;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(email.From, email.Password);
            client.Host = email.Host;
            client.Port = email.Port;

            try
            {
            
             client.Send(mailMessage);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Publishing domain event. Event - {message}", ex.Message);
                //throw;
                return false;
            }
            return true;

        }
    }
}
