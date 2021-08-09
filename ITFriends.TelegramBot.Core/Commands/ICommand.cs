using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ITFriends.TelegramBot.Core
{
    public interface ICommand
    {
        string Name { get; }
        Task Execute(Message message);
    }
}