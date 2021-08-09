using AutoMapper;
using ITFriends.Topic.Api.Requests;
using ITFriends.Topic.Core.Commands;

namespace ITFriends.Topic.Api.Profiles
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<CreateTopicRequest, CreateTopicCommand>();
            CreateMap<CreateTopicMessageRequest, CreateTopicMessageCommand>();
        }
    }
}