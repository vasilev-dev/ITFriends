using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class ForgotPasswordCommandRequestValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();
        }
    }
}