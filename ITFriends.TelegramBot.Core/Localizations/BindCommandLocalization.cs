using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public static class BindCommandLocalization
    {
        public static readonly ILocalization UsernameMustBeEntered = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "⚠ Username must be entered!"},
            {LanguageCode.Ru, "⚠ Необходимо ввести имя пользователя!"}
        });
        
        public static readonly ILocalization UserNotFound = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "⚠ The user with the specified name was not found!"},
            {LanguageCode.Ru, "⚠ Пользователь с указанным именем не найден!"}
        });
        
        public static readonly ILocalization TelegramAccountAlreadyIsBinded = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "⚠ Your telegram account is already binded!"},
            {LanguageCode.Ru, "⚠ Аккаунт telegram уже привязан!"}
        });

        public static readonly ILocalization SuccessBinding = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "✅ To finish linking your telegram account, go to [ITFriends]({0})"},
            {LanguageCode.Ru, "✅ Для окончания привязки telegram аккаунта перейдите на [ITFriends]({0})"}
        });
    }
}