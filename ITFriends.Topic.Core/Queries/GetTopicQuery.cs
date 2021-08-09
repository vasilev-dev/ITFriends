using ITFriends.Topic.Core.Dto;
using MediatR;

namespace ITFriends.Topic.Core.Queries
{
    public class GetTopicQuery : IRequest<GetTopicQueryDto>
    {
        public int TopicId { get; set; }
    }
}