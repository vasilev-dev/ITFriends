using System.Threading.Tasks;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public interface ITopicMessagePermissionValidator
    {
        Task ValidateAndThrowEditingPermissions(string appUserId, int topicMessageId);
    }
}