using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class ChangeRoleCommandRequestValidator : AbstractValidator<ChangeRoleCommand>
    {
        public ChangeRoleCommandRequestValidator()
        {
            RuleFor(c => c.AppUserId)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Role)
                .NotNull()
                .SetValidator(new AppUserRoleRequestValidator());
        }
    }
}