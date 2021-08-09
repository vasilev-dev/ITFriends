namespace ITFriends.Infrastructure.Configuration
{
    public class SmtpClientConfiguration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}