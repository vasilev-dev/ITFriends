using System.Threading;
using System.Threading.Tasks;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Topic.Core.Dto;
using ITFriends.Topic.Core.Queries;
using ITFriends.Topic.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Topic.Core.Handlers
{
    public class GetAppUserSubscriptionsQueryHandler : IRequestHandler<GetAppUserSubscriptionsQuery, GetAppUserSubscriptionsQueryDto>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetAppUserSubscriptionsQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetAppUserSubscriptionsQueryDto> Handle(GetAppUserSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _appUserRepository.GetSubscriptions(request.AppUserId);

            if (subscriptions == null)
            {
                throw new BusinessLogicValidationException(BusinessLogicErrors.ResourceNotFoundError, $"AppUser with id = {request.AppUserId} not found");
            }

            return new GetAppUserSubscriptionsQueryDto {Subscriptions = subscriptions};
        }
    }
}