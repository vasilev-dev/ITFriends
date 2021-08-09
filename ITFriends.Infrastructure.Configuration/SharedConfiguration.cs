namespace ITFriends.Infrastructure.Configuration
{
    public class SharedConfiguration
    {
        public ConnectionStringsConfiguration ConnectionStrings { get; set; }
        public ReadDbConfiguration ReadDbConfiguration { get; set; }
        public string IS4Url { get; set; }
        public string ClientUrl { get; set; }
        public RabbitMQConfiguration RabbitMqConfiguration { get; set; }
    }
}