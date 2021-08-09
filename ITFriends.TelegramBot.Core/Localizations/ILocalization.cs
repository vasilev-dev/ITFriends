using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public interface ILocalization
    {
        string this[string langCode] { get; }
    }
}