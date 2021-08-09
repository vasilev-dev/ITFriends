using System;

namespace ITFriends.Topic.Core.Dto
{
    public class TopicMessageDto
    {
        public int TopicMessageId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }
        public string AuthorUserName { get; set; }
    }
}