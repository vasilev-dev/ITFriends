using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class SubscribeToTopicDbSynchronizer : IConsumer<SubscribeToTopicEvent>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ITopicRepository _topicRepository;

        public SubscribeToTopicDbSynchronizer(IAppUserRepository appUserRepository, ITopicRepository topicRepository)
        {
            _appUserRepository = appUserRepository;
            _topicRepository = topicRepository;
        }

        public async Task Consume(ConsumeContext<SubscribeToTopicEvent> context)
        {
            var message = context.Message;

            var topicSubscription = new TopicSubscription
            {
                TopicId = message.TopicId,
                TopicTitle = message.TopicTitle
            };

            await _appUserRepository.AddSubscribeToTopic(message.AppUserId, topicSubscription);
            await _topicRepository.AddSubscriber(message.TopicId, message.AppUserId);
        }
    }
}