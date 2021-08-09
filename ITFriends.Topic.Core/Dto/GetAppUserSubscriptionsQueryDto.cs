using System.Collections.Generic;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Topic.Core.Dto
{
    public class GetAppUserSubscriptionsQueryDto
    {
        public List<TopicSubscription> Subscriptions { get; set; }
    }
}