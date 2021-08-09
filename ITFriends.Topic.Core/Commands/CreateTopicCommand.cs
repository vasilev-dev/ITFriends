using ITFriends.Infrastructure.SeedWork.BaseDto;
using MediatR;

namespace ITFriends.Topic.Core.Commands
{
    public class CreateTopicCommand : IRequest<BaseCreateResourceDto>
    {
        public int? ParentTopicId { get; set; }
        public string Title { get; set; }
    }
}