namespace ITFriends.Identity.Api.Requests
{
    public class ConfirmEmailRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}