using FluentValidation;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class TopicTitleValidator : AbstractValidator<string>
    {
        public TopicTitleValidator()
        {
            RuleFor(t => t)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}