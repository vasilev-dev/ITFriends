using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class LoginCommandBLValidator : IBusinessLogicValidator<LoginCommand>
    {
        private readonly IAppUserExistsValidator _appUserExistsValidator;

        public LoginCommandBLValidator(IAppUserExistsValidator appUserExistsValidator)
        {
            _appUserExistsValidator = appUserExistsValidator;
        }

        public async Task ValidateAndThrow(LoginCommand instance)
        {
            await _appUserExistsValidator.ValidateThatUserWithNameExistsAndThrow(instance.Username);
        }
    }
}