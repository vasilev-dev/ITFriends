using System.Collections.Generic;

namespace ITFriends.Infrastructure.Domain.Write
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        
        public int? ParentTopicId { get; set; }
        public Topic ParentTopic { get; set; }
        
        public ICollection<TopicMessage> TopicMessages { get; set; }
        
        public ICollection<Topic> SubTopics { get; set; }
        public ICollection<AppUser> Subscribers { get; set; } = new List<AppUser>();
    }
}