namespace ITFriends.Infrastructure.SeedWork.Events
{
    public class SendEmailEvent
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}