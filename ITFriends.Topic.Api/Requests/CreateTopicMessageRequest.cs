namespace ITFriends.Topic.Api.Requests
{
    public class CreateTopicMessageRequest
    {
        public long TopicId { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }
    }
}