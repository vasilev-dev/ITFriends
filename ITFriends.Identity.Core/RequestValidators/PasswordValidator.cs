using FluentValidation;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64)
                .Matches("[a-z]+")
                .Matches("[A-Z]+")
                .Matches("[0-9]+")
                .Matches("\\W+");
        }
    }
}