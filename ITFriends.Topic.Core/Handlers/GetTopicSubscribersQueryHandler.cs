using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Queries;
using ITFriends.Topic.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class GetTopicSubscribersQueryHandler : IRequestHandler<GetTopicSubscribersQuery, GetTopicSubscribersQueryDto>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicSubscribersQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetTopicSubscribersQueryDto> Handle(GetTopicSubscribersQuery request, CancellationToken cancellationToken)
        {
            var subscriberIds = await _topicRepository.GetTopicSubscriberIds(request.TopicId);

            if (subscriberIds == null)
            {
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"Topic with id = {request.TopicId} not found");
            }

            return new GetTopicSubscribersQueryDto {AppUserIds = subscriberIds};
        }
    }
}