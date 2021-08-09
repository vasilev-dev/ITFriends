using System;
using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public class Localization : ILocalization
    {
        public string this[string langCode] => GetLocalization(langCode);

        public Dictionary<string, string> LocalizationMap { get; set; }

        public Localization(Dictionary<string, string> localizationMap)
        {
            LocalizationMap = localizationMap;
        }
        
        public string GetLocalization(string langCode)
        {
            if (LocalizationMap.ContainsKey(langCode))
                return LocalizationMap[langCode];

            if (LocalizationMap.ContainsKey(LanguageCode.En))
                return LocalizationMap[LanguageCode.En];

            throw new Exception("English localization must be added");
        }
    }
}