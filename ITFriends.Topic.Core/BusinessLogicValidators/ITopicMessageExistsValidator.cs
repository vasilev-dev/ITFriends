using System.Threading;
using System.Threading.Tasks;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public interface ITopicMessageExistsValidator
    {
        Task TopicMessageWithIdExists(int topicMessageId);
    }
}