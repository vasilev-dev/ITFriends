using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.Events;
using ITFriends.TelegramBot.Core.Localizations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ITFriends.TelegramBot.Core.EventHandlers
{
    public class TopicMessageCreatedNotificationEventHandler : IConsumer<TopicMessageCreatedNotificationEvent>
    {
        private readonly AppDbContext _appDbContext;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly SharedConfiguration _sharedConfiguration;
        
        public TopicMessageCreatedNotificationEventHandler(
            AppDbContext appDbContext,
            TelegramBotClient telegramBotClient,
            SharedConfiguration sharedConfiguration)
        {
            _appDbContext = appDbContext;
            _telegramBotClient = telegramBotClient;
           _sharedConfiguration = sharedConfiguration;
        }

        public async Task Consume(ConsumeContext<TopicMessageCreatedNotificationEvent> context)
        {
            var message = context.Message;

            var chatIdOfSubscribers = await GetChatIdOfSubscribers(message.TopicId);
            
            var tasks = chatIdOfSubscribers.Select(x => _telegramBotClient.SendTextMessageAsync(
                x.chatId,
                FormatMessage(x.languageCode, message.TopicId, message.TopicMessageId, message.TopicTitle, message.Title, message.AuthorUserName),
                ParseMode.Markdown));

            await Task.WhenAll(tasks);
        }

        private async Task<List<(long chatId, string languageCode)>> GetChatIdOfSubscribers(int topicId)
        {
            // TODO Use Redis cache
            return await _appDbContext.TelegramBindings
                .Join(_appDbContext.Users, binding => binding.AppUserId, user => user.Id, (binding, user) => 
                    new {
                        binding.ChatId, 
                        binding.IsConfirmed,
                        binding.LanguageCode,
                        user.Subscriptions
                    })
                .Where(x => x.IsConfirmed && x.Subscriptions.Any(s => s.TopicId == topicId))
                .Select(x => new Tuple<long, string>(x.ChatId, x.LanguageCode).ToValueTuple())
                .ToListAsync();
        }
        
        private string FormatMessage(string langCode, int topicId, int topicMessageId, string topicTitle, string messageTitle, string author)
        {
            var linkTemplate = _sharedConfiguration.ClientUrl + "/topic/{0}/message/{1}/";
            var link = string.Format(linkTemplate, topicId, topicMessageId);
            
            return string.Format(
                NewTopicMessageNotificationLocalization.Notification[langCode],
                topicTitle, 
                messageTitle,
                link,
                author
            );
        }
    }
}