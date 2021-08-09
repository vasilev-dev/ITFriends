namespace ITFriends.Identity.Api.Requests
{
    public class RestorePasswordRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}