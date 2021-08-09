namespace ITFriends.Identity.Core.Events
{
    public class ChangeRoleEvent
    {
        public string AppUserId { get; set; }
        public string Role { get; set; }
    }
}