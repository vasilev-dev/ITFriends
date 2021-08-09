using System.Threading.Tasks;
using ITFriends.Identity.Core.Events;
using ITFriends.Identity.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Identity.Core.EventHandlers
{
    public class UserEmailConfirmedEventHandler : IConsumer<UserEmailConfirmedEvent>
    {
        private readonly IAppUserRepository _appUserRepository;

        public UserEmailConfirmedEventHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task Consume(ConsumeContext<UserEmailConfirmedEvent> context)
        {
            await _appUserRepository.UpdateEmailConfirmed(context.Message.UserId, true);
        }
    }
}