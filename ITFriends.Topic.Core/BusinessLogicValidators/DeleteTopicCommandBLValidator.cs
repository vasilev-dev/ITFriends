using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Commands;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class DeleteTopicCommandBLValidator : IBusinessLogicValidator<DeleteTopicCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITopicExistsValidator _topicExistsValidator;

        public DeleteTopicCommandBLValidator(AppDbContext appDbContext, ITopicExistsValidator topicExistsValidator)
        {
            _appDbContext = appDbContext;
            _topicExistsValidator = topicExistsValidator;
        }

        public async Task ValidateAndThrow(DeleteTopicCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicWithIdExistsAndThrow(instance.TopicId);
            await ValidateThatHasNotSubTopics(instance.TopicId);
        }

        private async Task ValidateThatHasNotSubTopics(int topicId)
        {
            var hasSubTopics = await _appDbContext.Topics.AnyAsync(t => t.ParentTopicId == topicId);

            if (hasSubTopics)
                throw new BusinessLogicValidationException(BusinessLogicErrors.CannotDeleteTopicError, $"Topic with id = {topicId} has subtopics");
        }
    }
}