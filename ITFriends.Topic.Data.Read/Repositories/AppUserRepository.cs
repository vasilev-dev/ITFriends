using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;
using MongoDB.Driver;

namespace ITFriends.Topic.Data.Read.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IMongoCollection<AppUser> _appUsers;

        public AppUserRepository(IMongoDatabase database)
        {
            _appUsers = database.GetCollection<AppUser>("AppUsers");
        }
        
        public async Task AddSubscribeToTopic(string appUserId, TopicSubscription topicSubscription)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == appUserId);
            var update = Builders<AppUser>.Update.Push(u => u.Subscriptions, topicSubscription);
            
            await _appUsers.FindOneAndUpdateAsync(filter, update);
        }

        public async Task UpdateTopicTitleInSubscriptions(int topicId, string title)
        {
            var filter = Builders<AppUser>.Filter.ElemMatch(u => u.Subscriptions, s => s.TopicId == topicId);
            var update = Builders<AppUser>.Update.Set(u => u.Subscriptions[-1].TopicTitle, title);

            await _appUsers.UpdateOneAsync(filter, update);
        }

        public async Task DeleteSubscribeToTopic(string appUserId, int topicId)
        {
            var filter = Builders<AppUser>.Filter.Where(u => u.Id == appUserId);
            var update = Builders<AppUser>.Update.PullFilter(u => u.Subscriptions, s => s.TopicId == topicId);

            await _appUsers.UpdateOneAsync(filter, update);
        }

        public async Task<List<TopicSubscription>> GetSubscriptions(string appUserId)
        {
            var filter = Builders<AppUser>.Filter.Where(t => t.Id == appUserId);
            var fields = Builders<AppUser>.Projection.Include(u => u.Subscriptions);
            var cursor = await _appUsers.FindAsync(filter, new FindOptions<AppUser> {Projection = fields});
            
            var appUser = await cursor.FirstOrDefaultAsync();
            
            return appUser?.Subscriptions;
        }
    }
}