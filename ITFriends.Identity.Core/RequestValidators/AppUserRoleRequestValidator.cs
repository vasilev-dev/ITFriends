using FluentValidation;
using ITFriends.Infrastructure.Domain.Common;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class AppUserRoleRequestValidator : AbstractValidator<string>
    {
        public AppUserRoleRequestValidator()
        {
            RuleFor(r => r)
                .NotEmpty()
                .Must(r => AppUserRole.All.Contains(r))
                    .WithMessage("Invalid role");
        }
    }
}