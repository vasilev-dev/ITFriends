using ITFriends.Identity.Core.Dto;
using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class ForgotPasswordCommand : IRequest<ForgotPasswordDto>
    {
        public string UserName { get; set; }
    }
}