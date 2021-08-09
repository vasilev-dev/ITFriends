using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Dto;
using ITFriends.Identity.Core.Events;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Domain.Common;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly SharedConfiguration _sharedConfiguration;
        private readonly InvitationEmailConfiguration _invitationEmailConfiguration; 

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, 
            SharedConfiguration sharedConfiguration, IPublishEndpoint publishEndpoint, 
            InvitationEmailConfiguration invitationEmailConfiguration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sharedConfiguration = sharedConfiguration;
            _publishEndpoint = publishEndpoint;
            _invitationEmailConfiguration = invitationEmailConfiguration;
        }

        public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            
            var newUser = new AppUser
            {
                Id = guid.ToString(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = false
            };
            
            var result = await _userManager.CreateAsync(newUser, request.Password);
            
            if (!result.Succeeded)
            {
                throw new Exception("Cannot create user");
            }
            
            await SaveUserRole(newUser, request.Role);
            await SaveUserClaims(newUser, request.Role);

            var savedUser = await _userManager.FindByNameAsync(newUser.UserName);
            
            await SendEmailInvitation(savedUser, cancellationToken);

            await PublishEvent(savedUser, request.Role, cancellationToken);

            return new RegisterUserDto {AppUserId = guid};
        }
        
        private async Task SaveUserRole(AppUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task SaveUserClaims(AppUser user, string role)
        {
            var claims = new List<Claim>
            {
                new("username", user.UserName),
                new("email", user.Email),
                new("firstname", user.FirstName),
                new("lastname", user.LastName),
                new("role", role)
            };
        
            await _userManager.AddClaimsAsync(user, claims);
        }

        private async Task SendEmailInvitation(AppUser user, CancellationToken cancellationToken)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var sendEmailEvent = new SendEmailEvent
            {
                ReceiverEmail = user.Email,
                Subject = _invitationEmailConfiguration.Subject,
                Body = InvitationEmailBody(token),
                IsBodyHtml = true,
            };
            
            await _publishEndpoint.Publish(sendEmailEvent, cancellationToken);
        }

        private string InvitationEmailBody(string token) => 
            string.Format(_invitationEmailConfiguration.EmailTemplate, _sharedConfiguration.ClientUrl, token);

        private async Task PublishEvent(AppUser user, string role, CancellationToken cancellationToken)
        {
            var userCreatedEvent = new UserCreatedEvent
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = new List<string> {role}
            };
            
            await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);
        }
    }
}