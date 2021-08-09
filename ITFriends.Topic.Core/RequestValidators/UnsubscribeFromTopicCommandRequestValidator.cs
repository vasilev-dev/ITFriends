using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class UnsubscribeFromTopicCommandRequestValidator : AbstractValidator<UnsubscribeFromTopicCommand>
    {
        public UnsubscribeFromTopicCommandRequestValidator()
        {
            RuleFor(c => c.AppUserId)
                .NotNull();
        }
    }
}