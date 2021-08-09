using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class CreateTopicMessageCommandBLValidator : IBusinessLogicValidator<CreateTopicMessageCommand>
    {
        private readonly ITopicExistsValidator _topicExistsValidator;
        private readonly IAppUserExistsValidator _appUserExistsValidator;

        public CreateTopicMessageCommandBLValidator(
            ITopicExistsValidator topicExistsValidator, 
            IAppUserExistsValidator appUserExistsValidator)
        {
            _topicExistsValidator = topicExistsValidator;
            _appUserExistsValidator = appUserExistsValidator;
        }

        public async Task ValidateAndThrow(CreateTopicMessageCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicWithIdExistsAndThrow(instance.TopicId);
            await _appUserExistsValidator.ValidateThatUserWithIdExistsAndThrow(instance.CreatorAppUserId);
        }
    }
}