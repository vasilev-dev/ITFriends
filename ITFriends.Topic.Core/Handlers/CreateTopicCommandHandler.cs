using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Common;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Infrastructure.SeedWork.BaseDto;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using MassTransit;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, BaseCreateResourceDto>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateTopicCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<BaseCreateResourceDto> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Infrastructure.Domain.Write.Topic
            {
                Title = request.Title,
                ParentTopicId = request.ParentTopicId
            };

            await _context.Topics.AddAsync(topic, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            await PublishEvent(topic, cancellationToken);

            return new BaseCreateResourceDto {CreatedResourceId = topic.TopicId};
        }

        private async Task PublishEvent(Infrastructure.Domain.Write.Topic topic, CancellationToken cancellationToken)
        {
            var parentTopicInfo = topic.ParentTopicId != null ? 
                _mapper.Map<ParentTopicInfoEventDto>(await _context.Topics.FindAsync(topic.ParentTopicId)) : null;
            
            var topicCreatedEvent = new TopicCreatedEvent
            {
                TopicId = topic.TopicId,
                Title = topic.Title,
                ParentTopicInfo = parentTopicInfo
            };
            
            await _publishEndpoint.Publish(topicCreatedEvent, cancellationToken);
        }
    }
}