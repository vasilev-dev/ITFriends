using ITFriends.Identity.Core.Dto;
using MediatR;

namespace ITFriends.Identity.Core.Queries
{
    public class GetUserQuery : IRequest<GetUserDto>
    {
        public string AppUserId { get; set; }
    }
}