using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITFriends.Infrastructure.Domain.Read;
using MongoDB.Driver;

namespace ITFriends.Topic.Data.Read.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly IMongoCollection<Infrastructure.Domain.Read.Topic> _topics;
        private readonly IMapper _mapper;

        public TopicRepository(IMongoDatabase database, IMapper mapper)
        {
            _mapper = mapper;
            _topics = database.GetCollection<Infrastructure.Domain.Read.Topic>("Topics");
        }
        
        public async Task InsertRootTopic(Infrastructure.Domain.Read.Topic topic)
        {
            await _topics.InsertOneAsync(topic);
        }

        public async Task InsertSubTopic(Infrastructure.Domain.Read.Topic topic)
        {
            var topicInfoForParent = _mapper.Map<TopicInfo>(topic);

            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topic.ParentTopicInfo.TopicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Push(t => t.SubTopicInfos, topicInfoForParent);

            await _topics.FindOneAndUpdateAsync(filter, update);
            await _topics.InsertOneAsync(topic);
        }

        public async Task<List<Infrastructure.Domain.Read.Topic>> GetRootTopics()
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.ParentTopicInfo == null);
            var cursor = await _topics.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<Infrastructure.Domain.Read.Topic> GetByTopicId(int topicId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var cursor = await _topics.FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task UpdateTitle(int topicId, string title)
        {
            await UpdateTitleInMainEntity(topicId, title);
            await UpdateTitleInSubTopicEntities(topicId, title);
            await UpdateTitleInParentEntity(topicId, title);
        }

        private async Task UpdateTitleInMainEntity(int topicId, string title)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Set(t => t.Title, title);

            await _topics.UpdateOneAsync(filter, update);
        }

        private async Task UpdateTitleInSubTopicEntities(int topicId, string title)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.ParentTopicInfo.TopicId == topicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Set(t => t.ParentTopicInfo.Title, title);

            await _topics.UpdateManyAsync(filter, update);
        }

        private async Task UpdateTitleInParentEntity(int topicId, string title)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.ElemMatch(t => t.SubTopicInfos, s => s.TopicId == topicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Set(t => t.SubTopicInfos[-1].Title, title);

            await _topics.UpdateOneAsync(filter, update);
        }
        
        public async Task InsertTopicMessageInfo(int topicId, TopicMessageInfo topicMessageInfo)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Push(t => t.TopicMessageInfos, topicMessageInfo);

            await _topics.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTopicMessageInfoTitle(int topicMessageId, string title)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.ElemMatch(t => t.TopicMessageInfos, m => m.TopicMessageId == topicMessageId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Set(t => t.TopicMessageInfos[-1].Title, title);

            await _topics.UpdateOneAsync(filter, update);
        }

        public async Task DeleteTopicMessageInfo(int topicMessageId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.ElemMatch(t => t.TopicMessageInfos, m => m.TopicMessageId == topicMessageId);
            var pull = Builders<Infrastructure.Domain.Read.Topic>.Update.PullFilter(t => t.TopicMessageInfos, m => m.TopicMessageId == topicMessageId);

            await _topics.UpdateOneAsync(filter, pull);
        }

        public async Task DeleteTopic(int topicId)
        {
            await DeleteSubTopicInfo(topicId);
            await _topics.DeleteOneAsync(t => t.TopicId == topicId);
        }
        
        private async Task DeleteSubTopicInfo(int topicId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.ElemMatch(t => t.SubTopicInfos, i =>  i.TopicId == topicId);
            var pull = Builders<Infrastructure.Domain.Read.Topic>.Update.PullFilter(t => t.SubTopicInfos, i =>  i.TopicId == topicId);
            
            await _topics.UpdateOneAsync(filter, pull);
        }
        
        public async Task AddSubscriber(int topicId, string appUserId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var update = Builders<Infrastructure.Domain.Read.Topic>.Update.Push(t => t.SubscriberIds, appUserId);

            await _topics.UpdateOneAsync(filter, update);
        }

        public async Task DeleteSubscriber(int topicId, string appUserId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var pull = Builders<Infrastructure.Domain.Read.Topic>.Update.Pull(t => t.SubscriberIds, appUserId);
            
            await _topics.UpdateOneAsync(filter, pull);
        }

        public async Task<List<string>> GetTopicSubscriberIds(int topicId)
        {
            var filter = Builders<Infrastructure.Domain.Read.Topic>.Filter.Where(t => t.TopicId == topicId);
            var fields = Builders<Infrastructure.Domain.Read.Topic>.Projection.Include(t => t.SubscriberIds);
            var cursor = await _topics.FindAsync(filter, new FindOptions<Infrastructure.Domain.Read.Topic> {Projection = fields});
            
            var topic = await cursor.FirstOrDefaultAsync();
            
            return topic?.SubscriberIds;
        }
    }
}