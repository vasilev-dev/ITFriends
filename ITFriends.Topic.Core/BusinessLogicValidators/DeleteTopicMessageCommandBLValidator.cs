using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class DeleteTopicMessageCommandBLValidator : IBusinessLogicValidator<DeleteTopicMessageCommand>
    {
        private readonly ITopicMessageExistsValidator _topicMessageExistsValidator;
        private readonly ITopicMessagePermissionValidator _permissionValidator;

        public DeleteTopicMessageCommandBLValidator(
            ITopicMessagePermissionValidator permissionValidator, 
            ITopicMessageExistsValidator topicMessageExistsValidator)
        {
            _permissionValidator = permissionValidator;
            _topicMessageExistsValidator = topicMessageExistsValidator;
        }

        public async Task ValidateAndThrow(DeleteTopicMessageCommand instance)
        {
            await _topicMessageExistsValidator.TopicMessageWithIdExists(instance.TopicMessageId);
            await _permissionValidator.ValidateAndThrowEditingPermissions(instance.EditorAppUserId, instance.TopicMessageId);
        }
    }
}