using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.TelegramBot.Core.Extensions;
using ITFriends.TelegramBot.Core.Localizations;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ITFriends.TelegramBot.Core.Bot
{
    public class Bot : IBot
    {
        private readonly TelegramBotClient _client;
        private readonly List<ICommand> _supportedCommands;
        private readonly ILogger _logger;

        public Bot(TelegramBotClient client, List<ICommand> supportedCommands, ILogger logger)
        {
            _client = client;
            _supportedCommands = supportedCommands;
            _logger = logger;
        }
        
        public async Task HandleMessage(Message message)
        {
            if (!message.IsCommand())
                return;

            var commandName = message.ParseCommandName();

            var command = GetCommand(commandName);

            if (command == null)
            {
                await _client.SendTextMessageAsync(message.Chat.Id, CommonLocalization.UnknownCommand[message.From.LanguageCode]);
                return;
            }

            try
            {
                await command.Execute(message);
            }
            catch (Exception e)
            {
                await _client.SendTextMessageAsync(message.Chat.Id, CommonLocalization.InternalError[message.From.LanguageCode]);
                _logger.Error(e.ToString());
            }
        }

        private ICommand GetCommand(string commandName)
        {
            return _supportedCommands.Find(c => c.Name == commandName);
        }
    }
}