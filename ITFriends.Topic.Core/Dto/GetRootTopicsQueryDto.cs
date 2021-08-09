using System.Collections.Generic;

namespace ITFriends.Topic.Core.Dto
{
    public class GetRootTopicsQueryDto
    {
        public List<Infrastructure.Domain.Read.Topic> RootTopics { get; set; }
    }
}