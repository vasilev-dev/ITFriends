using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class EditTopicMessageHtmlCommandRequestValidator : AbstractValidator<EditTopicMessageHtmlCommand>
    {
        public EditTopicMessageHtmlCommandRequestValidator(TopicMessageHtmlValidator topicMessageHtmlValidator)
        {
            RuleFor(c => c.Html)
                .NotNull()
                .SetValidator(topicMessageHtmlValidator);
            
            RuleFor(m => m.EditorAppUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}