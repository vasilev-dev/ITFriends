namespace ITFriends.Topic.Core.Events
{
    public class SubscribeToTopicEvent
    {
        public string AppUserId { get; set; }
        public int TopicId { get; set; }
        public string TopicTitle { get; set; }
    }
}