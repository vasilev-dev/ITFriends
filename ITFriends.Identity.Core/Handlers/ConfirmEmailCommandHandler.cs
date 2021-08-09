using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Events;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            
            if (!result.Succeeded)
                throw new BusinessLogicValidationException(BusinessLogicErrors.WrongTokenForEmailConfirmationError);

            await PublishEvent(user.Id, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(string userId, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new UserEmailConfirmedEvent {UserId = userId}, cancellationToken);
        }
    }
}