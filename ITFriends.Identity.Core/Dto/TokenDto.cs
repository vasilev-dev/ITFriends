using System;

namespace ITFriends.Identity.Core.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpirationTokenDateTime { get; set; }
        public DateTime? ExpirationRefreshTokenDateTime { get; set; }
        
        public bool HasError { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}