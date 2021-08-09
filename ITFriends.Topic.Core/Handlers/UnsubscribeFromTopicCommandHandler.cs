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
    public class UnsubscribeFromTopicCommandHandler : IRequestHandler<UnsubscribeFromTopicCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public UnsubscribeFromTopicCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UnsubscribeFromTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Infrastructure.Domain.Write.Topic {TopicId = request.TopicId};
            var subscriber = new AppUser {Id = request.AppUserId};

            // This is in memory, not connected.
            // So we do this because we try to tell EF that we want to remove this item
            topic.Subscribers.Add(subscriber);
            
            _context.Attach(topic);
            
            topic.Subscribers.Remove(subscriber);
            
            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(request.AppUserId, request.TopicId, cancellationToken);
            
            return Unit.Value;
        }
        
        private async Task PublishEvent(string appUserId, int topicId, CancellationToken cancellationToken)
        {
            var unsubscribeFromTopicEvent = new UnsubscribeFromTopicEvent
            {
                AppUserId = appUserId,
                TopicId = topicId,
            };
            
            await _publishEndpoint.Publish(unsubscribeFromTopicEvent, cancellationToken);
        }
    }
}