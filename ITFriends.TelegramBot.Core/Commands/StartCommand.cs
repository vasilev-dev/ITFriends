using System.Threading.Tasks;
using ITFriends.TelegramBot.Core.Localizations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ITFriends.TelegramBot.Core.Commands
{
    public class StartCommand : ICommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        
        public string Name => "/start";
        
        public StartCommand(TelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }
        
        public async Task Execute(Message message)
        {
            var chatId = message.Chat.Id;
            await _telegramBotClient.SendTextMessageAsync(chatId, StartCommandLocalization.Hello[message.From.LanguageCode], ParseMode.Html);
        }
    }
}