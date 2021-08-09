using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            
            if (!result.Succeeded)
                throw new BusinessLogicValidationException(BusinessLogicErrors.CannotChangePasswordError);
            
            return Unit.Value;
        }
    }
}