using AutoMapper;
using ITFriends.Identity.Api.Requests;
using ITFriends.Identity.Core.Commands;

namespace ITFriends.Identity.Api.Profiles
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUserCommand>();
            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();
            CreateMap<ConfirmEmailRequest, ConfirmEmailCommand>();
            CreateMap<RestorePasswordRequest, RestorePasswordCommand>();
            CreateMap<ConfirmTelegramBindingRequest, ConfirmTelegramBindingCommand>();
            CreateMap<ChangeRoleRequest, ChangeRoleCommand>();
        }
    }
}