using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Queries;
using ITFriends.Topic.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class GetRootTopicsQueryHandler : IRequestHandler<GetRootTopicsQuery, GetRootTopicsQueryDto>
    {
        private readonly ITopicRepository _topicRepository;

        public GetRootTopicsQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<GetRootTopicsQueryDto> Handle(GetRootTopicsQuery request, CancellationToken cancellationToken)
        {
            var rootTopic = await _topicRepository.GetRootTopics();

            return new GetRootTopicsQueryDto {RootTopics = rootTopic};
        }
    }
}