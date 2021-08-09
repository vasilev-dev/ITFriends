using ITFriends.Topic.Core.Dto;
using MediatR;

namespace ITFriends.Topic.Core.Queries
{
    public class GetAppUserSubscriptionsQuery : IRequest<GetAppUserSubscriptionsQueryDto>
    {
        public string AppUserId { get; set; }
    }
}