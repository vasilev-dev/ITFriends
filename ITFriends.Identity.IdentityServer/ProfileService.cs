using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using ITFriends.Infrastructure.Domain.Write;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private UserManager<AppUser> _userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var claims = await _userManager.GetClaimsAsync(user);
            
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }
    }
}