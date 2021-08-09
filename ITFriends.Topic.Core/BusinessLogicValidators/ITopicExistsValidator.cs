using System.Threading;
using System.Threading.Tasks;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public interface ITopicExistsValidator
    {
        Task ValidateThatTopicIsNullOrTExistsAndThrow(int? topicId);
        Task ValidateThatTopicWithIdExistsAndThrow(int topicId);
    }
}