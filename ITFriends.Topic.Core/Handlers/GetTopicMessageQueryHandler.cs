using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Queries;
using ITFriends.Topic.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class GetTopicMessageQueryHandler : IRequestHandler<GetTopicMessageQuery, TopicMessageDto>
    {
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly IMapper _mapper;

        public GetTopicMessageQueryHandler(ITopicMessageRepository topicMessageRepository, IMapper mapper)
        {
            _topicMessageRepository = topicMessageRepository;
            _mapper = mapper;
        }


        public async Task<TopicMessageDto> Handle(GetTopicMessageQuery request, CancellationToken cancellationToken)
        {
            var topicMessage = await _topicMessageRepository.Get(request.TopicMessageId);

            if (topicMessage == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"Topic message with id = {request.TopicMessageId} not found");

            return _mapper.Map<TopicMessageDto>(topicMessage);
        }
    }
}