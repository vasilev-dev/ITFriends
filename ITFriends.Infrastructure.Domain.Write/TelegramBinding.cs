namespace ITFriends.Infrastructure.Domain.Write
{
    public class TelegramBinding
    {
        public long ChatId { get; set; }
        public string TelegramUserName { get; set; }
        public string LanguageCode { get; set; }
        public bool IsConfirmed { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}