using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Dto;

namespace ITFriends.Topic.Api.Responses
{
    public class GetTopicMessageResponse
    {
        public TopicMessageDto TopicMessage { get; set; }
    }
}