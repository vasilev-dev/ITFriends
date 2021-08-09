using System;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Topic.Data.Read.Repositories
{
    public interface ITopicMessageRepository
    {
        Task Insert(TopicMessage topicMessage);
        Task<TopicMessage> Get(int topicMessageId);
        Task UpdateTitle(int topicMessageId, string title, DateTime updatedAt);
        Task UpdateHtmlBase64(int topicMessageId, string htmlBase64, DateTime updatedAt);
        Task Delete(int topicMessageId);
        Task DeleteAllInTopic(int topicId);
    }
}