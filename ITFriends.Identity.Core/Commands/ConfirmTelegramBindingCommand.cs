using MediatR;

namespace ITFriends.Identity.Core.Commands
{
    public class ConfirmTelegramBindingCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public long ChatId { get; set; }
    }
}