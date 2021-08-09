namespace ITFriends.Infrastructure.Configuration
{
    public class RabbitMQConfiguration
    {
        public string VHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }
    }
}