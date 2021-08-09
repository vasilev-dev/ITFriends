using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Configuration;
using Serilog;

namespace ITFriends.Notifier.Core.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly SmtpClientConfiguration _configuration;
        
        public SmtpEmailSender(ILogger logger, SmtpClientConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        
        public Task SendEmail(string receiverEmail, string body, string subject, bool isBodyHtml)
        {
            var from = new MailAddress(_configuration.Email, "ITFriends");
            var to = new MailAddress(receiverEmail);
            
            var message = new MailMessage(from, to)
            {
                Subject = subject, 
                Body = body, 
                IsBodyHtml = isBodyHtml
            };

            var smtp = new SmtpClient(_configuration.Host, _configuration.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration.Email, _configuration.Password),
                EnableSsl = true
            };

            try
            {
                smtp.Send(message);
            }
            catch(Exception exception)
            {
                _logger.Error(exception, "Error");
            }
            
            return Task.CompletedTask;
        }
    }
}