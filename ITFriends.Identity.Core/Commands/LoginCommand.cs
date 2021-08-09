using ITFriends.Identity.Core.Dto;
using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class LoginCommand : IRequest<TokenDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}