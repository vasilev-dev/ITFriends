namespace ITFriends.Identity.Api.Requests
{
    public class ChangeRoleRequest
    {
        public string AppUserId { get; set; }
        public string Role { get; set; }
    }
}