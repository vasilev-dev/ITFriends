using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ITFriends.Infrastructure.Domain.Read
{
    public class AppUser
    {
        [BsonId]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } 
        public bool EmailConfirmed { get; set; }
        public string TelegramUserName { get; set; }
        
        public List<string> Roles { get; set; }
        public List<TopicSubscription> Subscriptions { get; set; } = new();
    }
}