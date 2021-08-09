using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class DeleteTopicCommand : IRequest<Unit>
    {
        public int TopicId { get; set; }
    }
}