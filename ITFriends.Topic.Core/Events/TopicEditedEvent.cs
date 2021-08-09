namespace ITFriends.Topic.Core.Events
{
    public class TopicEditedEvent
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
    }
}