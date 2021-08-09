using AutoMapper;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Core.Events;

namespace ITFriends.Topic.Api.Profiles
{
    public class EventToModelProfile : Profile
    {
        public EventToModelProfile()
        {
            CreateMap<TopicMessageCreatedEvent, TopicMessage>();
            CreateMap<ParentTopicInfoEventDto, TopicInfo>();
        }
    }
}