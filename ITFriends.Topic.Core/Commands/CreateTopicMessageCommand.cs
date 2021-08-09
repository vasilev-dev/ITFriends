using ITFriends.Infrastructure.SeedWork.BaseDto;
using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class CreateTopicMessageCommand : IRequest<BaseCreateResourceDto>
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }
        public string CreatorAppUserId { get; set; }
    }
}