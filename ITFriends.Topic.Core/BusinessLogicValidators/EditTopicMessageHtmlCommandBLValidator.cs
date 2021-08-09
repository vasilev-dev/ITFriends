using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class EditTopicMessageHtmlCommandBLValidator : IBusinessLogicValidator<EditTopicMessageHtmlCommand>
    {
        private readonly ITopicMessageExistsValidator _topicMessageExistsValidator;
        private readonly ITopicMessagePermissionValidator _permissionValidator;

        public EditTopicMessageHtmlCommandBLValidator(
            ITopicMessagePermissionValidator validator, 
            ITopicMessageExistsValidator topicMessageExistsValidator)
        {
            _permissionValidator = validator;
            _topicMessageExistsValidator = topicMessageExistsValidator;
        }

        public async Task ValidateAndThrow(EditTopicMessageHtmlCommand instance)
        {
            await _topicMessageExistsValidator.TopicMessageWithIdExists(instance.TopicMessageId);
            await _permissionValidator.ValidateAndThrowEditingPermissions(instance.EditorAppUserId, instance.TopicMessageId);
        }
    }
}