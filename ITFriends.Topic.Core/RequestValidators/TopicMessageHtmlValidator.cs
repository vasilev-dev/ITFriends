using FluentValidation;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class TopicMessageHtmlValidator : AbstractValidator<string>
    {
        public TopicMessageHtmlValidator()
        {
            RuleFor(html => html)
                .NotEmpty();
        }
    }
}