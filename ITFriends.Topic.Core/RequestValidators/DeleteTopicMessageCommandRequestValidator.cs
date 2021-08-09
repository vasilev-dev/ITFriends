using FluentValidation;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Core.RequestValidators
{
    public class DeleteTopicMessageCommandRequestValidator : AbstractValidator<DeleteTopicMessageCommand>
    {
        public DeleteTopicMessageCommandRequestValidator()
        {
            RuleFor(m => m.EditorAppUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}