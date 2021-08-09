using System.Threading.Tasks;
using ITFriends.Identity.Core.Events;
using ITFriends.Identity.Data.Read.Repositories;
using MassTransit;

namespace ITFriends.Identity.Core.EventHandlers
{
    public class TelegramBindingConfirmedDbSynchronizer : IConsumer<TelegramBindingConfirmedEvent>
    {
        private readonly IAppUserRepository _appUserRepository;

        public TelegramBindingConfirmedDbSynchronizer(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task Consume(ConsumeContext<TelegramBindingConfirmedEvent> context)
        {
            var message = context.Message;
            
            await _appUserRepository.UpdateTelegramUserName(message.AppUserId, message.TelegramUserName);
        }
    }
}