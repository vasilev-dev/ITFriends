using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class EditTopicCommandBLValidator : IBusinessLogicValidator<EditTopicCommand>
    {
        private readonly ITopicExistsValidator _topicExistsValidator;

        public EditTopicCommandBLValidator(ITopicExistsValidator topicExistsValidator)
        {
            _topicExistsValidator = topicExistsValidator;
        }

        public async Task ValidateAndThrow(EditTopicCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicWithIdExistsAndThrow(instance.TopicId);
        }
    }
}