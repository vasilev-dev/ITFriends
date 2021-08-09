using FluentValidation;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandRequestValidator(IAppUserExistsValidator appUserExistsValidator)
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}