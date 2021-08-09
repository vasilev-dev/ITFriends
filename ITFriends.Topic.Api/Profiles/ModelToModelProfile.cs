using AutoMapper;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Events;

namespace ITFriends.Topic.Api.Profiles
{
    public class ModelToModelProfile : Profile
    {
        public ModelToModelProfile()
        {
            CreateMap<Infrastructure.Domain.Read.Topic, TopicInfo>()
                .ForMember(ti => ti.IsRoot, t => t.MapFrom(x => x.ParentTopicInfo == null));
            CreateMap<TopicMessage, TopicMessageInfo>();
        }
    }
}