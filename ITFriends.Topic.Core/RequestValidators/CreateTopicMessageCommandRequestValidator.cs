using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class CreateTopicMessageCommandRequestValidator : AbstractValidator<CreateTopicMessageCommand>
    {
        public CreateTopicMessageCommandRequestValidator(
            TopicMessageTitleValidator topicMessageTitleValidator, 
            TopicMessageHtmlValidator topicMessageHtmlValidator)
        {
            RuleFor(c => c.Title)
                .NotNull()
                .SetValidator(topicMessageTitleValidator);

            RuleFor(c => c.Html)
                .NotNull()
                .SetValidator(topicMessageHtmlValidator);

            RuleFor(c => c.CreatorAppUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}