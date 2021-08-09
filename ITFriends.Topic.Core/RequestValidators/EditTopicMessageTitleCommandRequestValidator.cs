using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class EditTopicMessageTitleCommandRequestValidator : AbstractValidator<EditTopicMessageTitleCommand>
    {
        public EditTopicMessageTitleCommandRequestValidator(TopicMessageTitleValidator topicMessageTitleValidator)
        {
            RuleFor(c => c.Title)
                .NotNull()
                .SetValidator(topicMessageTitleValidator);
            
            RuleFor(m => m.EditorAppUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}