using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class ConfirmEmailCommandRequestValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandRequestValidator(PasswordValidator passwordValidator)
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .SetValidator(passwordValidator);

            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty();
        }
    }
}