namespace ITFriends.Topic.Core.Events
{
    public class TopicCreatedEvent
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public ParentTopicInfoEventDto ParentTopicInfo { get; set; }
    }

    public class ParentTopicInfoEventDto
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public bool IsRoot { get; set; }
    }
}