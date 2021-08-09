using System.Collections.Generic;

namespace ITFriends.Topic.Api.Responses
{
    public class GetRootTopicsResponse
    {
        public List<Infrastructure.Domain.Read.Topic> Topics { get; set; }
    }
}