using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }
        public string UserId { get; set; }
    }
}