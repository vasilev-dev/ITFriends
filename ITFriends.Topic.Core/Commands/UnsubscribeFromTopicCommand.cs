using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class UnsubscribeFromTopicCommand : IRequest<Unit>
    {
        public string AppUserId { get; set; }
        public int TopicId { get; set; }
    }
}