using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Identity.Data.Read.Repositories
{
    public interface IAppUserRepository
    {
        Task Insert(AppUser user);
        Task UpdateEmailConfirmed(string userId, bool isConfirmed);
        Task UpdateTelegramUserName(string userId, string telegramUserName);
        Task<List<AppUser>> GetAllUsers();
        Task<AppUser> Get(string userId);
        Task UpdateRole(string userId, string role);
    }
}