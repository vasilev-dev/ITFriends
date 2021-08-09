using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class ChangePasswordCommandBLValidator : IBusinessLogicValidator<ChangePasswordCommand>
    {
        private readonly IAppUserExistsValidator _appUserExistsValidator;

        public ChangePasswordCommandBLValidator(IAppUserExistsValidator appUserExistsValidator)
        {
            _appUserExistsValidator = appUserExistsValidator;
        }

        public async Task ValidateAndThrow(ChangePasswordCommand instance)
        {
            await _appUserExistsValidator.ValidateThatUserWithIdExistsAndThrow(instance.UserId);
        }
    }
}