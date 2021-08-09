using System.Collections.Generic;
using ITFriends.Topic.Core.Dto;
using MediatR;

namespace ITFriends.Topic.Core.Queries
{
    public class GetTopicSubscribersQuery : IRequest<GetTopicSubscribersQueryDto>
    {
        public int TopicId { get; set; }
    }
}