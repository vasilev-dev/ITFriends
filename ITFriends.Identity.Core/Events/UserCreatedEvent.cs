using System.Collections.Generic;

namespace ITFriends.Identity.Core.Events
{
    public class UserCreatedEvent
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}