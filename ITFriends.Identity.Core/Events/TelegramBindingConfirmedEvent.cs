namespace ITFriends.Identity.Core.Events
{
    public class TelegramBindingConfirmedEvent
    {
        public string AppUserId { get; set; }
        public string TelegramUserName { get; set; }
    }
}