using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class EditTopicMessageHtmlCommand : IRequest<Unit>
    {
        public int TopicMessageId { get; set; }
        public string Html { get; set; }
        public string EditorAppUserId { get; set; }
    }
}