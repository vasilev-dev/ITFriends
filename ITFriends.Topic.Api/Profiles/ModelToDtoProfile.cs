using AutoMapper;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Services;

namespace ITFriends.Topic.Api.Profiles
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile(IBase64Converter base64Converter)
        {
            CreateMap<TopicMessage, TopicMessageDto>()
                .ForMember(m => m.Html,
                    o => o.MapFrom(s => base64Converter.DecodeFromBase64(s.HtmlBase64)));
        }
    }
}