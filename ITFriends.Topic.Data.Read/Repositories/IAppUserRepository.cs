using System.Collections.Generic;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Topic.Data.Read.Repositories
{
    public interface IAppUserRepository
    {
        Task AddSubscribeToTopic(string appUserId, TopicSubscription topicSubscription);
        Task UpdateTopicTitleInSubscriptions(int topicId, string title);
        Task DeleteSubscribeToTopic(string appUserId, int topicId);
        Task<List<TopicSubscription>> GetSubscriptions(string appUserId);
    }
}