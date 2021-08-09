using System.Collections.Generic;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Identity.Core.Dto
{
    public class GetAllUsersDto
    {
        public List<AppUser> Users { get; set; }
    }
}