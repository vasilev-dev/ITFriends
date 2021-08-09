using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Commands;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class UnsubscribeFromTopicCommandBLValidator : IBusinessLogicValidator<UnsubscribeFromTopicCommand>
    {
        private readonly ITopicExistsValidator _topicExistsValidator;
        private readonly IAppUserExistsValidator _appUserExistsValidator;
        private readonly AppDbContext _context;

        public UnsubscribeFromTopicCommandBLValidator(
            ITopicExistsValidator topicExistsValidator,
            IAppUserExistsValidator appUserExistsValidator, 
            AppDbContext context)
        {
            _topicExistsValidator = topicExistsValidator;
            _appUserExistsValidator = appUserExistsValidator;
            _context = context;
        }

        public async Task ValidateAndThrow(UnsubscribeFromTopicCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicWithIdExistsAndThrow(instance.TopicId);
            await _appUserExistsValidator.ValidateThatUserWithIdExistsAndThrow(instance.AppUserId);
            await ValidateThatUserSubscribedToTopicAndThrow(instance.AppUserId, instance.TopicId);
        }

        private async Task ValidateThatUserSubscribedToTopicAndThrow(string appUserId, int topicId)
        {
            var appUser = new AppUser {Id = appUserId};

            var subscribed = await _context.Topics.AnyAsync(t => t.TopicId == topicId && t.Subscribers.Contains(appUser));

            if (!subscribed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.NotSubscribedToTopicError, $"AppUser with id = {appUserId} not subscribed to topic with id = {topicId}");
        }
    }
}