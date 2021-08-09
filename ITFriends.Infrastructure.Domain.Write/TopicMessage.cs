using System;

namespace ITFriends.Infrastructure.Domain.Write
{
    public class TopicMessage
    {
        public int TopicMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string HtmlBase64 { get; set; }
        
        public string AuthorAppUserId { get; set; }
        public AppUser Author { get; set; }
        
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}