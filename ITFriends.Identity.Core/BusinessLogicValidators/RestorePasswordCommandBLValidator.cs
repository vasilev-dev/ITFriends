using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;

namespace ITFriends.Identity.Core.BusinessLogicValidators
{
    public class RestorePasswordCommandBLValidator : IBusinessLogicValidator<RestorePasswordCommand>
    {
        private readonly IAppUserExistsValidator _appUserExistsValidator;

        public RestorePasswordCommandBLValidator(IAppUserExistsValidator appUserExistsValidator)
        {
            _appUserExistsValidator = appUserExistsValidator;
        }

        public async Task ValidateAndThrow(RestorePasswordCommand instance)
        {
            await _appUserExistsValidator.ValidateThatUserWithNameExistsAndThrow(instance.UserName);
        }
    }
}