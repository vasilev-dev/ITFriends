namespace ITFriends.Infrastructure.SeedWork.Events
{
    public class TopicMessageCreatedNotificationEvent
    {
        public int TopicMessageId { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string AuthorUserName { get; set; }
        public string TopicTitle { get; set; }
    }
}