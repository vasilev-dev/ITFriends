using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class RefreshTokenCommandRequestValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}