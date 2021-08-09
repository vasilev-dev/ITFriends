namespace ITFriends.Topic.Core.Events
{
    public class UnsubscribeFromTopicEvent
    {
        public string AppUserId { get; set; }
        public int TopicId { get; set; }
    }
}