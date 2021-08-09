using System.Threading.Tasks;

namespace ITFriends.Notifier.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string receiverEmail, string body, string subject, bool isBodyHtml);
    }
}