using System;

namespace ITFriends.Infrastructure.Domain.Read
{
    public class TopicMessageInfo
    {
        public int TopicMessageId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorUserName { get; set; }
    }
}