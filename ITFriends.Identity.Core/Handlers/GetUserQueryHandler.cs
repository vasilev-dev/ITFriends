using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Dto;
using ITFriends.Identity.Core.Queries;
using ITFriends.Identity.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Identity.Core.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetUserQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _appUserRepository.Get(request.AppUserId);

            return new GetUserDto {User = user};
        }
    }
}