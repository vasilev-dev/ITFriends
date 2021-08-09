using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.EntityFrameworkCore;


namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class TopicExistsValidator : ITopicExistsValidator
    {
        private readonly AppDbContext _appDbContext;
        
        public TopicExistsValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task ValidateThatTopicIsNullOrTExistsAndThrow(int? topicId)
        {
            if (topicId == null)
                return;

            await ValidateThatTopicWithIdExistsAndThrow(topicId.Value);
        }

        public async Task ValidateThatTopicWithIdExistsAndThrow(int topicId)
        {
            var exists = await _appDbContext.Topics.AnyAsync(t => t.TopicId == topicId);

            if (!exists)
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"Topic with id = {topicId} not found");
        }
    }
}