using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Events;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Identity.Core.Handlers
{
    public class ConfirmTelegramBindingCommandHandler : IRequestHandler<ConfirmTelegramBindingCommand, Unit>
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public ConfirmTelegramBindingCommandHandler(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(ConfirmTelegramBindingCommand request, CancellationToken cancellationToken)
        {
            var userId = await _context.Users
                .Where(u => u.UserName == request.UserName)
                .Select(u => u.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var telegramUserName = await _context.TelegramBindings
                .Where(b => b.ChatId == request.ChatId && b.AppUserId == userId)
                .Select(b => b.TelegramUserName)
                .FirstOrDefaultAsync(cancellationToken);

            var telegramBinding = new TelegramBinding
            {
                ChatId = request.ChatId,
                AppUserId = userId,
                IsConfirmed = true
            };
            
            _context.TelegramBindings.Attach(telegramBinding).Property(b => b.IsConfirmed).IsModified = true;

            await _context.SaveChangesAsync(cancellationToken);
            
            await PublishEvent(userId, telegramUserName, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(string appUserId, string telegramUserName, CancellationToken cancellationToken)
        {
            var telegramBindingConfirmedEvent = new TelegramBindingConfirmedEvent
            {
                AppUserId = appUserId,
                TelegramUserName = telegramUserName
            };
            
            await _publishEndpoint.Publish(telegramBindingConfirmedEvent, cancellationToken);
        }
    }
}