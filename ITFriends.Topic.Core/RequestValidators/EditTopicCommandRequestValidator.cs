using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class EditTopicCommandRequestValidator : AbstractValidator<EditTopicCommand>
    {
        public EditTopicCommandRequestValidator(TopicTitleValidator topicTitleValidator)
        {
            RuleFor(c => c.Title)
                .NotNull()
                .SetValidator(topicTitleValidator);
        }
    }
}