namespace ITFriends.Topic.Api.Requests
{
    public class CreateTopicRequest
    {
        public long? ParentTopicId { get; set; }
        public string Title { get; set; }
    }
}