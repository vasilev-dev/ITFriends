using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class DeleteTopicMessageCommand : IRequest<Unit>
    {
        public int TopicMessageId { get; set; }
        public string EditorAppUserId { get; set; }
    }
}