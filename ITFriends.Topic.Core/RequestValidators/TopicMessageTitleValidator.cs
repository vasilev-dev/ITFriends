using FluentValidation;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class TopicMessageTitleValidator : AbstractValidator<string>
    {
        public TopicMessageTitleValidator()
        {
            RuleFor(title => title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);
        }
    }
}