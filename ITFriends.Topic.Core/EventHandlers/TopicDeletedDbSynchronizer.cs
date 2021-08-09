using System.Threading.Tasks;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicDeletedDbSynchronizer : IConsumer<TopicDeletedEvent>
    {
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly ITopicRepository _topicRepository;

        public TopicDeletedDbSynchronizer(
            ITopicMessageRepository topicMessageRepository, 
            ITopicRepository topicRepository)
        {
            _topicMessageRepository = topicMessageRepository;
            _topicRepository = topicRepository;
        }

        public async Task Consume(ConsumeContext<TopicDeletedEvent> context)
        {
            var topicId = context.Message.TopicId;

            await _topicMessageRepository.DeleteAllInTopic(topicId);
            await _topicRepository.DeleteTopic(topicId);
        }
    }
}