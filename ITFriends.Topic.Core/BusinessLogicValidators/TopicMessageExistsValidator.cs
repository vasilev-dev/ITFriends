using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class TopicMessageExistsValidator : ITopicMessageExistsValidator
    {
        private readonly AppDbContext _appDbContext;

        public TopicMessageExistsValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task TopicMessageWithIdExists(int topicMessageId)
        {
            var exists = await _appDbContext.TopicMessages.AnyAsync(m => m.TopicMessageId == topicMessageId);

            if (!exists)
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"TopicMessage with id = {topicMessageId} not found");
        }
    }
}