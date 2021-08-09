using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public static class CommonLocalization
    {
        public static readonly ILocalization UnknownCommand = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "🧐 Unknown command!"},
            {LanguageCode.Ru, "🧐 Неизвестная команда!"}
        });
        
        public static readonly ILocalization InternalError = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "🥵 Something went wrong! Try again later."},
            {LanguageCode.Ru, "🥵 Что-то пошло не так! Повторите позже."}
        });
    }
}