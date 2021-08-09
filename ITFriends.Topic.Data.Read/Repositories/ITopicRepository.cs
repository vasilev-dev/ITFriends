using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Topic.Data.Read.Repositories
{
    public interface ITopicRepository
    {
        Task InsertRootTopic(Infrastructure.Domain.Read.Topic topic);
        Task InsertSubTopic(Infrastructure.Domain.Read.Topic topic);
        Task<List<Infrastructure.Domain.Read.Topic>> GetRootTopics();
        Task<Infrastructure.Domain.Read.Topic> GetByTopicId(int topicId);
        Task UpdateTitle(int topicId, string title);
        Task InsertTopicMessageInfo(int topicId, TopicMessageInfo topicMessageInfo);
        Task UpdateTopicMessageInfoTitle(int topicMessageId, string title);
        Task DeleteTopicMessageInfo(int topicMessageId);
        Task DeleteTopic(int topicId);
        Task AddSubscriber(int topicId, string appUserId);
        Task DeleteSubscriber(int topicId, string appUserId);
        Task<List<string>> GetTopicSubscriberIds(int topicId);
    }
}