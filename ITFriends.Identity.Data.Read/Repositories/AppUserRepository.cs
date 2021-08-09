using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;
using MongoDB.Driver;

namespace ITFriends.Identity.Data.Read.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IMongoCollection<AppUser> _appUsers;

        public AppUserRepository(IMongoDatabase database)
        {
            _appUsers = database.GetCollection<AppUser>("AppUsers");
        }

        public async Task Insert(AppUser user)
        {
            await _appUsers.InsertOneAsync(user);
        }

        public async Task UpdateEmailConfirmed(string userId, bool isConfirmed)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == userId);
            var update = Builders<AppUser>.Update.Set(u => u.EmailConfirmed, isConfirmed);
            
            await _appUsers.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTelegramUserName(string userId, string telegramUserName)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == userId);
            var update = Builders<AppUser>.Update.Set(u => u.TelegramUserName, telegramUserName);

            await _appUsers.UpdateOneAsync(filter, update);
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var cursor = await _appUsers.FindAsync(_ => true);

            return await cursor.ToListAsync();
        }

        public async Task<AppUser> Get(string userId)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == userId);
            var cursor = await _appUsers.FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task UpdateRole(string userId, string role)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == userId);
            var update = Builders<AppUser>.Update.Set(u => u.Roles, new List<string> {role});

            await _appUsers.UpdateOneAsync(filter, update);
        }
    }
}