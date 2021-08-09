
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Infrastructure.Domain.Write
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<TopicMessage> TopicMessages { get; set; }
        public ICollection<Topic> Subscriptions { get; set; } = new List<Topic>();
        
        public TelegramBinding TelegramBinding { get; set; }
    }
}