using System.Collections.Generic;

namespace ITFriends.Topic.Core.Dto
{
    public class GetTopicSubscribersQueryDto
    {
        public List<string> AppUserIds { get; set; }
    }
}