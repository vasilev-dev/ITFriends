using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class EditTopicCommand : IRequest<Unit>
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
    }
}