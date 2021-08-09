using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ITFriends.Infrastructure.Configuration;
using ITFriends.TelegramBot.Core;
using ITFriends.TelegramBot.Core.Bot;
using ITFriends.TelegramBot.Core.Commands;
using Serilog;
using Telegram.Bot;

namespace ITFriends.TelegramBot.Api.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterTelegramBot(this ContainerBuilder containerBuilder, BotConfiguration botConfiguration, Assembly commandAssembly)
        {
            containerBuilder
                .Register(_ => botConfiguration);
            
            var telegramBotClient = new TelegramBotClient(botConfiguration.BotToken);
            telegramBotClient.SetWebhookAsync(botConfiguration.WebHookUrl + botConfiguration.WebHookEndpoint).Wait();

            containerBuilder
                .Register(_ => telegramBotClient)
                .SingleInstance();
            
            containerBuilder
                .RegisterAssemblyTypes(commandAssembly)
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .AsSelf()
                .AsImplementedInterfaces();
            
            containerBuilder
                .Register(c =>
                {
                    var client = c.Resolve<TelegramBotClient>();
                    
                    var commandTypes = c.ComponentRegistry.Registrations
                        .Where(r => typeof(ICommand).IsAssignableFrom(r.Activator.LimitType))
                        .Select(r => r.Activator.LimitType);
                    var command = commandTypes.Select(t => c.Resolve(t) as ICommand).ToList();

                    var logger = c.Resolve<ILogger>();
            
                    return new Bot(client, command, logger);
                })
                .As<IBot>()
                .SingleInstance();
        }
    }
}