using System.Collections.Generic;
using ITFriends.Infrastructure.Domain.Read;

namespace ITFriends.Topic.Api.Responses
{
    public class GetAppUserSubscriptionsResponse
    {
        public List<TopicSubscription> Subscriptions { get; set; }
    }
}