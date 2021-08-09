using AutoMapper;
using ITFriends.Topic.Core.Events;

namespace ITFriends.Topic.Api.Profiles
{
    public class ModelToEventProfile : Profile
    {
        public ModelToEventProfile()
        {
            CreateMap<Infrastructure.Domain.Write.Topic, ParentTopicInfoEventDto>()
                .ForMember(ti => ti.IsRoot, t => t.MapFrom(x => x.ParentTopicId == null));
        }
    }
}