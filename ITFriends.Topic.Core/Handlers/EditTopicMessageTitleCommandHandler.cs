using System;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using MassTransit;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class EditTopicMessageTitleCommandHandler : IRequestHandler<EditTopicMessageTitleCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public EditTopicMessageTitleCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(EditTopicMessageTitleCommand request, CancellationToken cancellationToken)
        {
            var updatedMessage = new TopicMessage
            {
                TopicMessageId = request.TopicMessageId,
                UpdatedAt = DateTime.UtcNow,
                Title = request.Title
            };

            _context.TopicMessages.Attach(updatedMessage).Property(t => t.Title).IsModified = true;
            _context.TopicMessages.Attach(updatedMessage).Property(t => t.UpdatedAt).IsModified = true;
            
            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(updatedMessage, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(TopicMessage updatedTopicMessage, CancellationToken cancellationToken)
        {
            var topicMessageTitleEditedEvent = new TopicMessageTitleEditedEvent
            {
                TopicMessageId = updatedTopicMessage.TopicMessageId,
                UpdatedAt = updatedTopicMessage.UpdatedAt.Value,
                Title = updatedTopicMessage.Title
            };
            
            await _publishEndpoint.Publish(topicMessageTitleEditedEvent, cancellationToken);
        }
    }
}