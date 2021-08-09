using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicMessageHtmlEditedDbSynchronizer : IConsumer<TopicMessageHtmlEditedEvent>
    {
        private readonly ITopicMessageRepository _topicMessageRepository;

        public TopicMessageHtmlEditedDbSynchronizer(ITopicMessageRepository topicMessageRepository)
        {
            _topicMessageRepository = topicMessageRepository;
        }

        public async Task Consume(ConsumeContext<TopicMessageHtmlEditedEvent> context)
        {
            var message = context.Message;
            
            await _topicMessageRepository.UpdateHtmlBase64(message.TopicMessageId, message.HtmlBase64, message.UpdatedAt);
        }
    }
}