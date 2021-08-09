using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Topic.Core.Commands;
using ITFriends.Topic.Core.Events;
using MassTransit;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class EditTopicCommandHandler : IRequestHandler<EditTopicCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public EditTopicCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(EditTopicCommand request, CancellationToken cancellationToken)
        {
            await UpdateTitle(request.TopicId, request.Title, cancellationToken);

            await PublishEvent(request.TopicId, request.Title, cancellationToken);
            
            return Unit.Value;
        }

        private async Task UpdateTitle(int topicId, string title, CancellationToken cancellationToken)
        {
            var topic = new Infrastructure.Domain.Write.Topic {TopicId = topicId, Title = title};
            _context.Topics.Attach(topic).Property(t => t.Title).IsModified = true;

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishEvent(int topicId, string title, CancellationToken cancellationToken)
        {
            var topicEditedEvent = new TopicEditedEvent
            {
                TopicId = topicId,
                Title = title
            };
            
            await _publishEndpoint.Publish(topicEditedEvent, cancellationToken);
        }
    }
}