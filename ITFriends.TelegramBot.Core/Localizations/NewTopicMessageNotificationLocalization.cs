using System.Collections.Generic;

namespace ITFriends.TelegramBot.Core.Localizations
{
    public class NewTopicMessageNotificationLocalization
    {
        public static readonly ILocalization Notification = new Localization(new Dictionary<string, string>
        {
            {LanguageCode.En, "✉ New message in *{0}* topic:\n\n[{1}]({2}) _by {3}_"},
            {LanguageCode.Ru, "✉ Новое сообщение в *{0}* топике:\n\n[{1}]({2}) _от {3}_"}
        });
    }
}