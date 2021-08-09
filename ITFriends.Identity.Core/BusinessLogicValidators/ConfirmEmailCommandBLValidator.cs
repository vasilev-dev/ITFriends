using System;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class ConfirmEmailCommandBLValidator : IBusinessLogicValidator<ConfirmEmailCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmEmailCommandBLValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAndThrow(ConfirmEmailCommand instance)
        {
            await ValidateThatUserExistsAndPasswordAndEmailNotConfirmedAndThrow(instance.UserName, instance.Password);
        }
        
        private async Task ValidateThatUserExistsAndPasswordAndEmailNotConfirmedAndThrow(string userName, string password)
        {
            if (userName == null)
                throw new NullReferenceException();
            
            var user = await _userManager.FindByNameAsync(userName);
            
            if (user == null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);
            
            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            
            if (result == PasswordVerificationResult.Failed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserNotFoundError);
            
            if (user.EmailConfirmed)
                throw new BusinessLogicValidationException(BusinessLogicErrors.EmailAlreadyConfirmedError);
        }
    }
}