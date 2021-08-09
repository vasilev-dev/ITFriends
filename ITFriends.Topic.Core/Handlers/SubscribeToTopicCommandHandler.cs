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
    public class SubscribeToTopicCommandHandler : IRequestHandler<SubscribeToTopicCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public SubscribeToTopicCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SubscribeToTopicCommand request, CancellationToken cancellationToken)
        {
            var subscriber = new AppUser {Id = request.AppUserId};
            _context.Attach(subscriber);

            var topic = await _context.Topics.FindAsync(request.TopicId);

            topic.Subscribers.Add(subscriber);

            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(request.AppUserId, request.TopicId, topic.Title, cancellationToken);

            return Unit.Value;
        }
        
        private async Task PublishEvent(string appUserId, int topicId, string topicTitle, CancellationToken cancellationToken)
        {
            var subscribeToTopicEvent = new SubscribeToTopicEvent
            {
                AppUserId = appUserId,
                TopicId = topicId,
                TopicTitle = topicTitle
            };
            
            await _publishEndpoint.Publish(subscribeToTopicEvent, cancellationToken);
        }
    }
}