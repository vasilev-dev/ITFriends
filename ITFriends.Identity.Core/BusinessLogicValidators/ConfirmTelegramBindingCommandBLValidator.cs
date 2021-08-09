using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class ConfirmTelegramBindingCommandBLValidator : IBusinessLogicValidator<ConfirmTelegramBindingCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public ConfirmTelegramBindingCommandBLValidator(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public async Task ValidateAndThrow(ConfirmTelegramBindingCommand instance)
        {
            var user = await _userManager.FindByNameAsync(instance.UserName);
            
            await ValidateThatUserExistsAndPasswordIsCorrectAndThrow(user, instance.Password);
            await ValidateThatTelegramBindingExistsAndNotConfirmed(instance.ChatId, user.Id);
        }

        private async Task ValidateThatUserExistsAndPasswordIsCorrectAndThrow(AppUser user, string password)
        {
            if (user == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);

            var success = await _userManager.CheckPasswordAsync(user, password);

            if (!success)
                throw new BusinessLogicValidationException(BusinessLogicErrors.WrongUserNameOrPasswordError);
        }

        private async Task ValidateThatTelegramBindingExistsAndNotConfirmed(long chatId, string appUserId)
        {
            var telegramBinding = await _context.TelegramBindings.AsNoTracking()
                .FirstOrDefaultAsync(b => b.AppUserId == appUserId && b.ChatId == chatId);

            if (telegramBinding == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError,
                    $"TopicBinding with ChatId = {chatId} and AppUserId = {appUserId} not found!");

            if (telegramBinding.IsConfirmed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.TelegramBindingAlreadyConfirmedError,
                    $"TopicBinding with ChatId = {chatId} and AppUserId = {appUserId} already confirmed!");
        }
    }
}
