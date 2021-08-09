using System;

namespace ITFriends.Topic.Core.Events
{
    public class TopicMessageHtmlEditedEvent
    {
        public int TopicMessageId { get; set; }
        public string HtmlBase64 { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}