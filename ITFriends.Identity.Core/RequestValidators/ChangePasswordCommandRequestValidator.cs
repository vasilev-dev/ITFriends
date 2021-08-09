using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class ChangePasswordCommandRequestValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandRequestValidator(PasswordValidator passwordValidator)
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.OldPassword)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.NewPassword)
                .SetValidator(passwordValidator);
            
            RuleFor(x => x.NewConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.NewPassword);
        }
    }
}