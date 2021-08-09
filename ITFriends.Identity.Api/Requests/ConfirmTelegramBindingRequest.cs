namespace ITFriends.Identity.Api.Requests
{
    public class ConfirmTelegramBindingRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public long ChatId { get; set; }
    }
}