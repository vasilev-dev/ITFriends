using ITFriends.Infrastructure.Domain.Read;
using MongoDB.Driver;

namespace ITFriends.Infrastructure.Data.Read.Migrations
{
    public static class ReadDbMigrations
    {
        public static void ExecuteMigrations(IMongoDatabase database)
        {
            AppUserCollectionMigrations(database);
            TopicCollectionMigrations(database);
            TopicMessageCollectionMigrations(database);
        }

        private static void AppUserCollectionMigrations(IMongoDatabase database)
        {
            var collection = database.GetCollection<AppUser>("AppUsers");

            var userNameIndex = Builders<AppUser>.IndexKeys.Ascending(user => user.UserName);
            var emailIndex = Builders<AppUser>.IndexKeys.Ascending(user => user.Email);
            
            collection.Indexes.CreateOne(new CreateIndexModel<AppUser>(userNameIndex, new CreateIndexOptions {Unique = true}));
            collection.Indexes.CreateOne(new CreateIndexModel<AppUser>(emailIndex));
        }
        
        private static void TopicCollectionMigrations(IMongoDatabase database)
        {
            
        }
        
        private static void TopicMessageCollectionMigrations(IMongoDatabase database)
        {
           
        }
    }
}