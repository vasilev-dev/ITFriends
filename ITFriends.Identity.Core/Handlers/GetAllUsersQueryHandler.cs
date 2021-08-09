using System.Threading;
using System.Threading.Tasks;
using ITFriends.Identity.Core.Dto;
using ITFriends.Identity.Core.Queries;
using ITFriends.Identity.Data.Read.Repositories;
using MediatR;

namespace ITFriends.Identity.Core.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersDto>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetAllUsersQueryHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetAllUsersDto> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _appUserRepository.GetAllUsers();
            
            return new GetAllUsersDto {Users = users};
        }
    }
}