using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicMessageDeletedDbSynchronizer : IConsumer<TopicMessageDeletedEvent>
    {
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly ITopicRepository _topicRepository;

        public TopicMessageDeletedDbSynchronizer(ITopicMessageRepository topicMessageRepository, ITopicRepository topicRepository)
        {
            _topicMessageRepository = topicMessageRepository;
            _topicRepository = topicRepository;
        }

        public async Task Consume(ConsumeContext<TopicMessageDeletedEvent> context)
        {
            var topicMessageId = context.Message.TopicMessageId;

            await _topicMessageRepository.Delete(topicMessageId);
            await _topicRepository.DeleteTopicMessageInfo(topicMessageId);
        }
    }
}