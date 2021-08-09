using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ITFriends.TelegramBot.Core.Bot
{
    public interface IBot
    {
        Task HandleMessage(Message message);
    }
}