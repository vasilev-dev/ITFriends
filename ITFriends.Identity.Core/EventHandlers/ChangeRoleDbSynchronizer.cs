using System.Threading.Tasks;
using ITFriends.Identity.Core.Events;
using ITFriends.Identity.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Identity.Core.EventHandlers
{
    public class ChangeRoleDbSynchronizer : IConsumer<ChangeRoleEvent>
    {
        private readonly IAppUserRepository _appUserRepository;

        public ChangeRoleDbSynchronizer(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task Consume(ConsumeContext<ChangeRoleEvent> context)
        {
            var message = context.Message;
            
            await _appUserRepository.UpdateRole(message.AppUserId, message.Role);
        }
    }
}