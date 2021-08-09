using System.Threading.Tasks;
using ITFriends.TelegramBot.Core.Localizations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ITFriends.TelegramBot.Core.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        
        public string Name => "/help";
        
        public HelpCommand(TelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }
        
        public async Task Execute(Message message)
        {
            await _telegramBotClient.SendTextMessageAsync( message.Chat.Id, HelpCommandLocalization.Help[message.From.LanguageCode], ParseMode.Html);
        }
    }
}