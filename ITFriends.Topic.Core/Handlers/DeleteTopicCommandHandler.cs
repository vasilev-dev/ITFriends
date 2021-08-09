using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using MassTransit;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteTopicCommandHandler(AppDbContext appDbContext, IPublishEndpoint publishEndpoint)
        {
            _context = appDbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var deletedTopic = new Infrastructure.Domain.Write.Topic {TopicId = request.TopicId};
            
            _context.Topics.Remove(deletedTopic);

            await _context.SaveChangesAsync(cancellationToken);

            await PublishEvent(request.TopicId);
            
            return Unit.Value;
        }

        private async Task PublishEvent(int topicId)
        {
            await _publishEndpoint.Publish(new TopicDeletedEvent {TopicId = topicId});
        }
    }
}