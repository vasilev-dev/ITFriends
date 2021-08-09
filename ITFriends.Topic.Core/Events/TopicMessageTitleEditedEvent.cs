using System;

namespace ITFriends.Topic.Core.Events
{
    public class TopicMessageTitleEditedEvent
    {
        public int TopicMessageId { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}