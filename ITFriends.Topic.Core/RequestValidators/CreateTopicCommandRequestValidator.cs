using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class CreateTopicCommandRequestValidator : AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicCommandRequestValidator(TopicTitleValidator topicTitleValidator)
        {
            RuleFor(c => c.Title)
                .NotNull()
                .SetValidator(topicTitleValidator);
        }
    }
}