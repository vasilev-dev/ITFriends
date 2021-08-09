using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class ChangeRoleCommand : IRequest<Unit>
    {
        public string AppUserId { get; set; }
        public string Role { get; set; }
    }
}