using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.BaseDto;
using ITFriends.Infrastructure.SeedWork.Events;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using ITFriends.Topic.Core.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.Handlers
{
    public class CreateTopicMessageCommandHandler : IRequestHandler<CreateTopicMessageCommand, BaseCreateResourceDto>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBase64Converter _base64Converter;

        public CreateTopicMessageCommandHandler(
            AppDbContext context,
            IPublishEndpoint publishEndpoint,
            IBase64Converter base64Converter)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _base64Converter = base64Converter;
        }

        public async Task<BaseCreateResourceDto> Handle(CreateTopicMessageCommand request, CancellationToken cancellationToken)
        {
            var topicMessage = new TopicMessage
            {
                TopicId = request.TopicId,
                Title = request.Title,
                HtmlBase64 = _base64Converter.EncodeToBase64(request.Html),
                AuthorAppUserId = request.CreatorAppUserId,
                CreatedAt = DateTime.UtcNow
            };
            
            await _context.TopicMessages.AddAsync(topicMessage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvents(topicMessage, request.CreatorAppUserId, cancellationToken);

            return new BaseCreateResourceDto {CreatedResourceId = topicMessage.TopicMessageId};
        }

        private async Task PublishEvents(TopicMessage topicMessage, string userId, CancellationToken cancellationToken)
        {
            var authorUserName = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .SingleOrDefaultAsync(cancellationToken);

            var topicName = await _context.Topics
                .Where(t => t.TopicId == topicMessage.TopicId).Select(t => t.Title)
                .FirstOrDefaultAsync(cancellationToken);

            await PublishSyncEvent(topicMessage, authorUserName, cancellationToken);
            await PublishNotificationEvent(topicMessage, authorUserName, topicName, cancellationToken);
        }
        
        private async Task PublishSyncEvent(TopicMessage topicMessage, string authorUserName, CancellationToken cancellationToken)
        {
            var topicMessageCreatedEvent = new TopicMessageCreatedEvent
            {
                TopicMessageId = topicMessage.TopicMessageId,
                TopicId = topicMessage.TopicId,
                Title = topicMessage.Title,
                AuthorUserName = authorUserName,
                CreatedAt = topicMessage.CreatedAt,
                HtmlBase64 = topicMessage.HtmlBase64
            };

            await _publishEndpoint.Publish(topicMessageCreatedEvent, cancellationToken);
        }

        private async Task PublishNotificationEvent(TopicMessage topicMessage, string authorUserName, string topicTitle, CancellationToken cancellationToken)
        {
            var topicMessageCreatedNotificationEvent = new TopicMessageCreatedNotificationEvent
            {
                TopicMessageId = topicMessage.TopicMessageId,
                TopicId = topicMessage.TopicId,
                Title = topicMessage.Title,
                TopicTitle = topicTitle,
                AuthorUserName = authorUserName
            };

            await _publishEndpoint.Publish(topicMessageCreatedNotificationEvent, cancellationToken);
        }
    }
}