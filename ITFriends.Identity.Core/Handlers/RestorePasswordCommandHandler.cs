using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class RestorePasswordCommandHandler : IRequestHandler<RestorePasswordCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public RestorePasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<Unit> Handle(RestorePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            
            if (!result.Succeeded)
                throw new BusinessLogicValidationException(BusinessLogicErrors.InvalidRestoreTokenError);
            
            return Unit.Value;
        }
    }
}