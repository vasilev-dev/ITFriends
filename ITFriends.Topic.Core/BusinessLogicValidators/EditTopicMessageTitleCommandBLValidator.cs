using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class EditTopicMessageTitleCommandBLValidator : IBusinessLogicValidator<EditTopicMessageTitleCommand>
    {
        private readonly ITopicMessageExistsValidator _topicMessageExistsValidator;
        private readonly ITopicMessagePermissionValidator _permissionValidator;

        public EditTopicMessageTitleCommandBLValidator(
            ITopicMessagePermissionValidator permissionValidator, 
            ITopicMessageExistsValidator topicMessageExistsValidator)
        {
            _permissionValidator = permissionValidator;
            _topicMessageExistsValidator = topicMessageExistsValidator;
        }

        public async Task ValidateAndThrow(EditTopicMessageTitleCommand instance)
        {
            await _topicMessageExistsValidator.TopicMessageWithIdExists(instance.TopicMessageId);
            await _permissionValidator.ValidateAndThrowEditingPermissions(instance.EditorAppUserId, instance.TopicMessageId);
        }
    }
}