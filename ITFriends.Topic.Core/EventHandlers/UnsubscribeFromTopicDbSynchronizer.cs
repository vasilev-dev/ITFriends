using System.Threading.Tasks;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class UnsubscribeFromTopicDbSynchronizer : IConsumer<UnsubscribeFromTopicEvent>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ITopicRepository _topicRepository;

        public UnsubscribeFromTopicDbSynchronizer(IAppUserRepository appUserRepository, ITopicRepository topicRepository)
        {
            _appUserRepository = appUserRepository;
            _topicRepository = topicRepository;
        }

        public async Task Consume(ConsumeContext<UnsubscribeFromTopicEvent> context)
        {
            var message = context.Message;

            await _appUserRepository.DeleteSubscribeToTopic(message.AppUserId, message.TopicId);
            await _topicRepository.DeleteSubscriber(message.TopicId, message.AppUserId);
        }
    }
}