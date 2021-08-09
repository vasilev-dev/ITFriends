using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class ConfirmEmailCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}