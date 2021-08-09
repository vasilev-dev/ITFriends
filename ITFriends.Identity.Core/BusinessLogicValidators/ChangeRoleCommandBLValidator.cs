using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Common;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Errors;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class ChangeRoleCommandBLValidator : IBusinessLogicValidator<ChangeRoleCommand>
    {
        private readonly IAppUserExistsValidator _appUserExistsValidator;

        public ChangeRoleCommandBLValidator(IAppUserExistsValidator appUserExistsValidator)
        {
            _appUserExistsValidator = appUserExistsValidator;
        }

        public async Task ValidateAndThrow(ChangeRoleCommand instance)
        {
            await _appUserExistsValidator.ValidateThatUserWithIdExistsAndThrow(instance.AppUserId);
            ValidateAndThrowAppUserRole(instance.Role);
        }

        private void ValidateAndThrowAppUserRole(string role)
        {
            if (role == AppUserRole.Admin)
                throw new BusinessLogicValidationException(BusinessLogicErrors.CannotSetAdminRoleError);
        }
    }
}