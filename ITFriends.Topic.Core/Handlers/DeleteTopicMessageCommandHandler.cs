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
    public class DeleteTopicMessageCommandHandler : IRequestHandler<DeleteTopicMessageCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteTopicMessageCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteTopicMessageCommand request, CancellationToken cancellationToken)
        {
            var deletedMessage = new TopicMessage {TopicMessageId = request.TopicMessageId};
            
            _context.TopicMessages.Remove(deletedMessage);

            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(request.TopicMessageId, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(int topicMessageId, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new TopicMessageDeletedEvent {TopicMessageId = topicMessageId}, cancellationToken);
        }
    }
}