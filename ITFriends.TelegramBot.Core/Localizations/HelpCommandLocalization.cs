using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public class HelpCommandLocalization
    {
        public static readonly ILocalization Help = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "❔ <b>Available commands:</b>\n<i>/start</i> - start bot\n<i>/bind username - bind itfriends to telegram</i>\n<i>/help - help by commands</i>"},
            {LanguageCode.Ru, "❔ <b>Доступные команды:</b>\n<i>/start</i> - начать общение с ботом\n<i>/bind никнейм</i> - привязать itfriends к telegram\n<i>/help</i> - помощь по командам" 
            }
        });
    }
}