using System;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Core.Services;
using MassTransit;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class EditTopicMessageHtmlCommandHandler : IRequestHandler<EditTopicMessageHtmlCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IBase64Converter _base64Converter;
        private readonly IPublishEndpoint _publishEndpoint;
        public EditTopicMessageHtmlCommandHandler(
            AppDbContext context, 
            IBase64Converter base64Converter, 
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _base64Converter = base64Converter;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(EditTopicMessageHtmlCommand request, CancellationToken cancellationToken)
        {
            var updatedMessage = new TopicMessage
            {
                TopicMessageId = request.TopicMessageId,
                UpdatedAt = DateTime.UtcNow,
                HtmlBase64 = _base64Converter.EncodeToBase64(request.Html)
            };

            _context.TopicMessages.Attach(updatedMessage).Property(m => m.HtmlBase64).IsModified = true;
            _context.TopicMessages.Attach(updatedMessage).Property(t => t.UpdatedAt).IsModified = true;

            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(updatedMessage, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(TopicMessage updatedTopicMessage, CancellationToken cancellationToken)
        {
            var topicMessageHtmlEditedEvent = new TopicMessageHtmlEditedEvent
            {
                TopicMessageId = updatedTopicMessage.TopicMessageId,
                UpdatedAt = updatedTopicMessage.UpdatedAt.Value,
                HtmlBase64 = updatedTopicMessage.HtmlBase64
            };
            
            await _publishEndpoint.Publish(topicMessageHtmlEditedEvent, cancellationToken);
        }
    }
}