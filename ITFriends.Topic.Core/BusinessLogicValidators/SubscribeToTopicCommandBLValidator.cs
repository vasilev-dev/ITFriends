using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Commands;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class SubscribeToTopicCommandBLValidator : IBusinessLogicValidator<SubscribeToTopicCommand>
    {
        private readonly ITopicExistsValidator _topicExistsValidator;
        private readonly IAppUserExistsValidator _appUserExistsValidator;
        private readonly AppDbContext _context;

        public SubscribeToTopicCommandBLValidator(
            ITopicExistsValidator topicExistsValidator,
            IAppUserExistsValidator appUserExistsValidator, 
            AppDbContext context)
        {
            _topicExistsValidator = topicExistsValidator;
            _appUserExistsValidator = appUserExistsValidator;
            _context = context;
        }

        public async Task ValidateAndThrow(SubscribeToTopicCommand instance)
        {
            await _topicExistsValidator.ValidateThatTopicWithIdExistsAndThrow(instance.TopicId);
            await _appUserExistsValidator.ValidateThatUserWithIdExistsAndThrow(instance.AppUserId);
            await ValidateThatAppUserNotSubscribedToTopicAndThrow(instance.AppUserId, instance.TopicId);
        }

        private async Task ValidateThatAppUserNotSubscribedToTopicAndThrow(string appUserId, int topicId)
        {
            var appUser = new AppUser {Id = appUserId};

            var alreadySubscribed = await _context.Topics.AnyAsync(t => t.TopicId == topicId && t.Subscribers.Contains(appUser));

            if (alreadySubscribed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.AlreadySubscribedToTopicError, $"AppUser with id = {appUserId} already subscribed to topic with id = {topicId}");
        }
    }
}