using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Topic.Core.EventHandlers
{
    public class TopicCreatedDbSynchronizer : IConsumer<TopicCreatedEvent>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicCreatedDbSynchronizer(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TopicCreatedEvent> context)
        {
            var message = context.Message;

            var topic = new Infrastructure.Domain.Read.Topic
            {
                TopicId = message.TopicId,
                Title = message.Title,
                ParentTopicInfo = _mapper.Map<TopicInfo>(message.ParentTopicInfo),
                SubTopicInfos = new List<TopicInfo>(),
                TopicMessageInfos = new List<TopicMessageInfo>(),
                SubscriberIds = new List<string>()
            };
            
            if (topic.ParentTopicInfo == null)
                await _topicRepository.InsertRootTopic(topic);
            else
                await _topicRepository.InsertSubTopic(topic);
        }
    }
}