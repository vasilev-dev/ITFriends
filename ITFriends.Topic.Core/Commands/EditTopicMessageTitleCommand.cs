using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class EditTopicMessageTitleCommand : IRequest<Unit>
    {
        public int TopicMessageId { get; set; }
        public string Title { get; set; }
        public string EditorAppUserId { get; set; }
    }
}