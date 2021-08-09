using System;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Common;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class RegisterUserCommandBLValidator : IBusinessLogicValidator<RegisterUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserCommandBLValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAndThrow(RegisterUserCommand instance)
        {
            await ValidateThatUserNameIsFreeAndThrow(instance.UserName);
            ValidateAndThrowAppUserRole(instance.Role);
        }
        
        private async Task ValidateThatUserNameIsFreeAndThrow(string userName)
        {
            if (userName == null)
                throw new NullReferenceException();
            
            var user = await _userManager.FindByNameAsync(userName);
            
            if (user != null)
                throw new BusinessLogicValidationException(BusinessLogicErrors.UserAlreadyRegisteredError);
        }
        
        private void ValidateAndThrowAppUserRole(string role)
        {
            if (role != AppUserRole.User)
                throw new BusinessLogicValidationException(BusinessLogicErrors.CannotSetNotUserRoleError);
        }
    }
}