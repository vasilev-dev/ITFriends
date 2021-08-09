using AutoMapper;
using ITFriends.Identity.Api.Requests;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Api.Profiles
{
    public class RequestToQueryProfile : Profile
    {
        public RequestToQueryProfile()
        {
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
            CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();
        }
    }
}