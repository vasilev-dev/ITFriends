using ITFriends.Identity.Core.Dto;
using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string RefreshToken { get; set; }
    }
}