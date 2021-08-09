using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Dto;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITFriends.Identity.Core.Handlers
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly RestorePasswordEmailConfiguration _restorePasswordEmailConfiguration;
        private readonly SharedConfiguration _sharedConfiguration;

        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager, IPublishEndpoint publishEndpoint, 
            RestorePasswordEmailConfiguration restorePasswordEmailConfiguration, SharedConfiguration sharedConfiguration)
        {
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
            _restorePasswordEmailConfiguration = restorePasswordEmailConfiguration;
            _sharedConfiguration = sharedConfiguration;
        }

        public async Task<ForgotPasswordDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await SendRestorePasswordEmail(user.Email, token, cancellationToken);
            
            return new ForgotPasswordDto { Email = user.Email };
        }

        private async Task SendRestorePasswordEmail(string email, string token, CancellationToken cancellationToken)
        {
            var sendEmailEvent = new SendEmailEvent
            {
                ReceiverEmail = email,
                Subject = _restorePasswordEmailConfiguration.Subject,
                Body = GenerateRestorePasswordEmailBody(token),
                IsBodyHtml = true,
            };
            
            await _publishEndpoint.Publish(sendEmailEvent, cancellationToken);
        }

        private string GenerateRestorePasswordEmailBody(string token) =>
            string.Format(_restorePasswordEmailConfiguration.EmailTemplate, _sharedConfiguration.ClientUrl, token);
    }
}