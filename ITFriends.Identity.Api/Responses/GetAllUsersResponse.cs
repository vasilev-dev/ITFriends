using System.Collections.Generic;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Identity.Api.Responses
{
    public class GetAllUsersResponse
    {
        public List<AppUser> Users { get; set; }
    }
}