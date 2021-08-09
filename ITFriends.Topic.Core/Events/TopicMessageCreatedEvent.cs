using System;

namespace ITFriends.Topic.Core.Events
{
    public class TopicMessageCreatedEvent
    {
        public int TopicMessageId { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string HtmlBase64 { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime CreatedAt{ get; set; }
    }
}