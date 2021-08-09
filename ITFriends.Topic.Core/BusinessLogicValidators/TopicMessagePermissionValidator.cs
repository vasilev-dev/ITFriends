using System;
using System.Linq;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Common;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Topic.Core.BusinessLogicValidators
{
    public class TopicMessagePermissionValidator : ITopicMessagePermissionValidator
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TopicMessagePermissionValidator(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task ValidateAndThrowEditingPermissions(string appUserId, int topicMessageId)
        {
            if (await UserIsAuthor(appUserId, topicMessageId))
                return;
            
            if (await UserIsModeratorOrAdmin(appUserId))
                return;
            
            throw new SecurityException(appUserId, "TopicMessage", topicMessageId, 
                $"User with id = {appUserId} without editing permission tried to edit TopicMessage with id = {topicMessageId}.");
        }

        private async Task<bool> UserIsModeratorOrAdmin(string appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId);

            if (user == null)
                throw new NullReferenceException($"User with id = {appUserId} not found");
            
            var userRoles = await _userManager.GetRolesAsync(user);
            
            return userRoles.Any(role => role == AppUserRole.Admin || role == AppUserRole.Moderator);
        }

        private async Task<bool> UserIsAuthor(string appUserId, int topicMessageId)
        {
            return await _context.TopicMessages
                .AnyAsync(m => m.TopicMessageId == topicMessageId && m.AuthorAppUserId == appUserId);
        }
    }
}