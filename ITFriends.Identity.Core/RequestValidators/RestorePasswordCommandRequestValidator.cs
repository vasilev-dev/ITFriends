using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class RestorePasswordCommandRequestValidator : AbstractValidator<RestorePasswordCommand>
    {
        public RestorePasswordCommandRequestValidator(PasswordValidator passwordValidator)
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Token)
                .NotNull();
            
            RuleFor(c => c.Password).NotNull()
                .SetValidator(passwordValidator);

            RuleFor(c => c.ConfirmPassword)
                .NotNull()
                .Equal(c => c.Password);
        }
    }
}