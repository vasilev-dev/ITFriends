using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace ITFriends.TelegramBot.Core.Extensions
{
    public static class MessageExtensions
    {
        public static bool IsCommand(this Message message) => message.Type == Telegram.Bot.Types.Enums.MessageType.Text 
                                                              && message.Text.StartsWith("/");
        
        public static (string commandName, List<string> commandParameters) ParseCommand(this Message message)
        {
            if (!IsCommand(message))
                throw new ArgumentException("Is not command");
            
            var splittedMessage = message.Text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            return (splittedMessage[0], splittedMessage[1..].ToList());
        }

        public static string ParseCommandName(this Message message)
        {
            var (commandName, _) = ParseCommand(message);

            return commandName;
        }

        public static List<string> ParseCommandParameters(this Message message)
        {
            var (_, commandParameters) = ParseCommand(message);

            return commandParameters;
        }
    }
}