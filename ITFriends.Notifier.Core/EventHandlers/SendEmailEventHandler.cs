using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.Events;
using ITFriends.Notifier.Core.Services;
using MassTransit;

namespace ITFriends.Notifier.Core.EventHandlers
{
    public class SendEmailEventHandler : IConsumer<SendEmailEvent>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        
        public async Task Consume(ConsumeContext<SendEmailEvent> context)
        {
            var message = context.Message;
            
            await _emailSender.SendEmail(message.ReceiverEmail, message.Body, message.Subject, message.IsBodyHtml);
        }
    }
}