using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ITFriends.Infrastructure.Domain.Read
{
    public class Topic
    {
        [BsonId]
        public int TopicId { get; set; }
        public string Title { get; set; }
        public TopicInfo ParentTopicInfo { get; set; }
        public List<TopicInfo> SubTopicInfos { get; set; } = new();
        public List<TopicMessageInfo> TopicMessageInfos { get; set; } = new();
        public List<string> SubscriberIds { get; set; } = new();
    }
}