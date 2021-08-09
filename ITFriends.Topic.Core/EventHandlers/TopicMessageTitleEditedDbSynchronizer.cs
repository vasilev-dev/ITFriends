using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicMessageTitleEditedDbSynchronizer : IConsumer<TopicMessageTitleEditedEvent>
    {
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly ITopicRepository _topicRepository;

        public TopicMessageTitleEditedDbSynchronizer(ITopicMessageRepository topicMessageRepository, ITopicRepository topicRepository)
        {
            _topicMessageRepository = topicMessageRepository;
            _topicRepository = topicRepository;
        }

        public async Task Consume(ConsumeContext<TopicMessageTitleEditedEvent> context)
        {
            var message = context.Message;
            
            await _topicMessageRepository.UpdateTitle(message.TopicMessageId, message.Title, message.UpdatedAt);
            await _topicRepository.UpdateTopicMessageInfoTitle(message.TopicMessageId, message.Title);
        }
    }
}