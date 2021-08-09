using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicMessageCreatedDbSynchronizer : IConsumer<TopicMessageCreatedEvent>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly IMapper _mapper;

        public TopicMessageCreatedDbSynchronizer(
            ITopicRepository topicRepository,
            ITopicMessageRepository topicMessageRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _topicMessageRepository = topicMessageRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TopicMessageCreatedEvent> context)
        {
            var message = context.Message;
            
            var topicMessage = _mapper.Map<TopicMessage>(message);
            await _topicMessageRepository.Insert(topicMessage);
            
            var topicMessageInfo = _mapper.Map<TopicMessageInfo>(topicMessage);
            await _topicRepository.InsertTopicMessageInfo(message.TopicId, topicMessageInfo);
        }
    }
}