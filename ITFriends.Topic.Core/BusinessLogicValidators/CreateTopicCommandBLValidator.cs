using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class CreateTopicCommandBLValidator : IBusinessLogicValidator<CreateTopicCommand>
    {
        private readonly ITopicExistsValidator _topicExistsValidator;

        public CreateTopicCommandBLValidator(ITopicExistsValidator topicExistsValidator)
        {
            _topicExistsValidator = topicExistsValidator;
        }

        public async Task ValidateAndThrow(CreateTopicCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicIsNullOrTExistsAndThrow(instance.ParentTopicId);
        }
    }
}