using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ITFriends.Infrastructure.Domain.Read
{
    public class TopicMessage
    {
        [BsonId]
        public int TopicMessageId { get; set; }
        public int TopicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string HtmlBase64 { get; set; }
        public string AuthorUserName { get; set; }
    }
}