using System;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class ForgotPasswordCommandBLValidator : IBusinessLogicValidator<ForgotPasswordCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordCommandBLValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAndThrow(ForgotPasswordCommand instance)
        {
            await ValidateThatUserExistsAndEmailConfirmedAndThrow(instance.UserName);
        }
        
        private async Task ValidateThatUserExistsAndEmailConfirmedAndThrow(string userName)
        {
            if (userName == null)
                throw new NullReferenceException();
            
            var user = await _userManager.FindByNameAsync(userName);
            
            if (user == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);
            
            if (!user.EmailConfirmed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.EmailNotConfirmedError);
        }
    }
}