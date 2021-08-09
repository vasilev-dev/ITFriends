using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class SubscribeToTopicCommandRequestValidator : AbstractValidator<SubscribeToTopicCommand>
    {
        public SubscribeToTopicCommandRequestValidator()
        {
            RuleFor(c => c.AppUserId)
                .NotNull();
        }
    }
}