using FluentValidation;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Core.RequestValidators
{
    public class ConfirmTelegramBindingCommandRequestValidator : AbstractValidator<ConfirmTelegramBindingCommand>
    {
        public ConfirmTelegramBindingCommandRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}