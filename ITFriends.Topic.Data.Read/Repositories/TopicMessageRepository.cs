using System;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Data.Read.Repositories;
using MongoDB.Driver;

namespace ITFriends.Infrastructure.Data.Repositories.Read
{
    public class TopicMessageRepository : ITopicMessageRepository
    {
        private readonly IMongoCollection<TopicMessage> _topicMessages;

        public TopicMessageRepository(IMongoDatabase database)
        {
            _topicMessages = database.GetCollection<TopicMessage>("TopicMessages");
        }

        public async Task Insert(TopicMessage topicMessage)
        {
            await _topicMessages.InsertOneAsync(topicMessage);
        }

        public async Task<TopicMessage> Get(int topicMessageId)
        {
            var filter = Builders<TopicMessage>.Filter.Where(m => m.TopicMessageId == topicMessageId);
            var cursor = await _topicMessages.FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task UpdateTitle(int topicMessageId, string title, DateTime updatedAt)
        {
            var filter = Builders<TopicMessage>.Filter.Where(m => m.TopicMessageId == topicMessageId);
            var update = Builders<TopicMessage>.Update
                .Set(m => m.Title, title)
                .Set(m => m.UpdatedAt, updatedAt);

            await _topicMessages.UpdateOneAsync(filter, update);
        }

        public async Task UpdateHtmlBase64(int topicMessageId, string htmlBase64, DateTime updatedAt)
        {
            var filter = Builders<TopicMessage>.Filter.Where(m => m.TopicMessageId == topicMessageId);
            var update = Builders<TopicMessage>.Update
                .Set(m => m.HtmlBase64, htmlBase64)
                .Set(m => m.UpdatedAt, updatedAt);;

            await _topicMessages.UpdateOneAsync(filter, update);
        }

        public async Task Delete(int topicMessageId)
        {
            await _topicMessages.DeleteOneAsync(m => m.TopicMessageId == topicMessageId);
        }

        public async Task DeleteAllInTopic(int topicId)
        {
            await _topicMessages.DeleteManyAsync(m => m.TopicId == topicId);
        }
    }
}