using System;
using System.Linq;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.TelegramBot.Core.Extensions;
using ITFriends.TelegramBot.Core.Localizations;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ITFriends.TelegramBot.Core.Commands
{
    public class BindCommand : ICommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly AppDbContext _context;
        private readonly SharedConfiguration _sharedConfiguration;

        public string Name => "/bind";

        public BindCommand(
            TelegramBotClient telegramBotClient,
            AppDbContext context,
            SharedConfiguration sharedConfiguration)
        {
            _telegramBotClient = telegramBotClient;
            _context = context;
            _sharedConfiguration = sharedConfiguration;
        }
        
        public async Task Execute(Message message)
        {
            var parameters = message.ParseCommandParameters();

            if (!parameters.Any())
            {
                await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, 
                    BindCommandLocalization.UsernameMustBeEntered[message.From.LanguageCode]);
                return;
            }
            
            var userName = parameters[0];

            var user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

            if (user == null)
            {
                await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, 
                    BindCommandLocalization.UserNotFound[message.From.LanguageCode]);
                return;
            }

            var existingBind = await _context.TelegramBindings.FirstOrDefaultAsync(b =>
                    b.AppUserId == user.Id && b.ChatId == message.Chat.Id);

            var successMessage = GenerateSuccessBindingLink(message, user.UserName);
            
            if (existingBind != null)
            {
                if (existingBind.IsConfirmed)
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                        BindCommandLocalization.TelegramAccountAlreadyIsBinded[message.From.LanguageCode]);
                else
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, successMessage, ParseMode.Markdown);

                return;
            }

            var binding = new TelegramBinding
            {
                ChatId = message.Chat.Id,
                TelegramUserName = message.From.Username,
                LanguageCode = message.From.LanguageCode,
                AppUserId = user.Id,
                IsConfirmed = false
            };

            await _context.TelegramBindings.AddAsync(binding);
            await _context.SaveChangesAsync();
            
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, successMessage, ParseMode.Markdown);
        }

        private string GenerateSuccessBindingLink(Message message, string userName)
        {
            var linkTemplate = _sharedConfiguration.ClientUrl + "/telegram/bind?chat_id={0}&username={1}/";
            var link = string.Format(linkTemplate, message.Chat.Id, userName);
            
            return string.Format(BindCommandLocalization.SuccessBinding[message.From.LanguageCode], link, link); // telegram not render link if url hasn't domain zone (not work for localhost)
        }
    }
}