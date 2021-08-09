using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Events;
using ITFriends.Infrastructure.Domain.Write;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public ChangeRoleCommandHandler(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AppUserId);

            await _userManager.RemoveFromRoleAsync(user, request.Role);
            
            var oldRoleClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == "role");
            if (oldRoleClaim != null)
                await _userManager.RemoveClaimAsync(user, oldRoleClaim);
            
            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new IdentityRole(request.Role));

            await _userManager.AddToRoleAsync(user, request.Role);
            await _userManager.AddClaimAsync(user, new Claim("role", request.Role));

            await PublishEvent(request.AppUserId, request.Role, cancellationToken);
            
            return Unit.Value;
        }

        private async Task PublishEvent(string appUserId, string role, CancellationToken cancellationToken)
        {
             var changeRoleEvent = new ChangeRoleEvent
             {
                AppUserId = appUserId,
                Role = role
             };

             await _publishEndpoint.Publish(changeRoleEvent, cancellationToken);
        }
    }
}