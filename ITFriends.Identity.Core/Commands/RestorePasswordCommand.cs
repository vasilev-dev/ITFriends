using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class RestorePasswordCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}