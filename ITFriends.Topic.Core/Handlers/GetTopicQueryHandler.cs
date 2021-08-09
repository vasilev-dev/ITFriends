using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Queries;
using ITFriends.Topic.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class GetTopicQueryHandler : IRequestHandler<GetTopicQuery, GetTopicQueryDto>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetTopicQueryDto> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        {
            var topic =  await _topicRepository.GetByTopicId(request.TopicId);
            
            if (topic == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"Topic with id = {request.TopicId} not found");

            return new GetTopicQueryDto {Topic = topic};
        }
    }
}