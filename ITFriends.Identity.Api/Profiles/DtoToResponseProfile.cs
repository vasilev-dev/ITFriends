using AutoMapper;
using ITFriends.Identity.Api.Responses;
using ITFriends.Identity.Core.Dto;

namespace ITFriends.Identity.Api.Profiles
{
    public class DtoToResponseProfile : Profile
    {
        public DtoToResponseProfile()
        {
            CreateMap<TokenDto, TokenResponse>();
            CreateMap<ForgotPasswordDto, ForgotPasswordResponse>();
        }
    }
}