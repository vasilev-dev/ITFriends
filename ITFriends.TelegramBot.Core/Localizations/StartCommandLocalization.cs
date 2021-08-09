using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public static class StartCommandLocalization
    {
        public static readonly ILocalization Hello = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "🖐 <b>Hello!</b>\n\nTo bind your telegram account to ITFriends, enter the <b>/bind</b> <i>your_username</i> command.\n\nHelp by commands - <b>/help</b>."},
            {LanguageCode.Ru, "🖐 <b>Привет!</b>\n\nДля того чтобы привязать аккаунт telegram к ITFriends, введите команду <b>/bind</b> <i>ваш_никнейм</i>.\n\nПомощь по командам - <b>/help</b>."}
        });
    }
}