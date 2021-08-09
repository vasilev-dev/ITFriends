using System.Threading.Tasks;
using ITFriends.TelegramBot.Core;
using ITFriends.TelegramBot.Core.Bot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace ITFriends.TelegramBot.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IBot _bot;

        public MessageController(IBot bot)
        {
            _bot = bot;
        }

        [AllowAnonymous]
        [HttpPost("message/handle")]
        public async Task<IActionResult> Handle([FromBody] Update update)
        {
            if (update == null)
                return Ok();

            await _bot.HandleMessage(update.Message);
            
            return Ok();
        }
    }
}