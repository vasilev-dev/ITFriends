using FluentValidation;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Common;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class RegisterUserCommandRequestValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandRequestValidator(PasswordValidator passwordValidator)
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(32);

            RuleFor(c => c.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(c => c.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(c => c.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotNull()
                .SetValidator(passwordValidator);

            RuleFor(c => c.ConfirmPassword)
                .NotNull()
                .Equal(c => c.Password);
            
            RuleFor(c => c.Role)
                .NotNull()
                .SetValidator(new AppUserRoleRequestValidator());
        }
    }
}