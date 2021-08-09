using System.Threading.Tasks;
using ITFriends.Identity.Core.Events;
using ITFriends.Identity.Data.Read.Repositories;
using ITFriends.Infrastructure.Domain.Read;
using MassTransit;

namespace ITFriends.Identity.Core.EventHandlers
{
    public class UserCreatedDbSynchronizer : IConsumer<UserCreatedEvent>
    {
        private readonly IAppUserRepository _appUserRepository;

        public UserCreatedDbSynchronizer(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var message = context.Message;
            
            var readUser = new AppUser
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                UserName = message.UserName,
                Email = message.Email,
                TelegramUserName = null,
                EmailConfirmed = false,
                Roles = message.Roles
            };

            await _appUserRepository.Insert(readUser);
        }
    }
}