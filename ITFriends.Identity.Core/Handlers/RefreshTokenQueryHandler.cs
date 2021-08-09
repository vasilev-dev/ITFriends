using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using ITFriends.Identity.Core.Commands;
using ITFriends.Identity.Core.Dto;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ITFriends.Identity.Core.Handlers
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly string _is4Url;
        private readonly string _clientId;

        public RefreshTokenQueryHandler(IConfiguration configuration)
        {
            _is4Url = configuration["IS4Url"] ?? throw new ArgumentException("IS4Url is not set");
            _clientId = configuration["ClientId"] ?? throw new ArgumentException("ClientId is not set");
        }
        
        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_is4Url, cancellationToken);
            
            if (disco.IsError)
                throw new Exception($"Cannot connect to IdentityServer by {_is4Url} address");
            
            var tokenResponse = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _clientId,
                GrantType = "refresh_token",
                Parameters = 
                {
                    {"refresh_token", $"{request.RefreshToken}"}
                }
            }, cancellationToken);
            
            var response = new TokenDto
            {
                Token = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                ExpirationTokenDateTime = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
                ExpirationRefreshTokenDateTime = DateTime.Now.AddDays(30), // tokenResponse doesn't contains this info
                HasError = tokenResponse.IsError,
                Error = tokenResponse.Error,
                ErrorDescription = tokenResponse.ErrorDescription
            };

            return response;
        }
    }
}