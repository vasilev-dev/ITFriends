using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicEditedDbSynchronizer : IConsumer<TopicEditedEvent>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IAppUserRepository _appUserRepository;

        public TopicEditedDbSynchronizer(
            ITopicRepository topicRepository,
            IAppUserRepository appUserRepository)
        {
            _topicRepository = topicRepository;
            _appUserRepository = appUserRepository;
        }


        public async Task Consume(ConsumeContext<TopicEditedEvent> context)
        {
            var message = context.Message;

            await _topicRepository.UpdateTitle(message.TopicId, message.Title);
            await _appUserRepository.UpdateTopicTitleInSubscriptions(message.TopicId, message.Title);
        }
    }
}