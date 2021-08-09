using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Dto;
using MediatR;

namespace ITFriends.Topic.Core.Queries
{
    public class GetTopicMessageQuery : IRequest<TopicMessageDto>
    {
        public int TopicMessageId { get; set; }
    }
}