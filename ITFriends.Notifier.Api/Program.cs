using Autofac.Extensions.DependencyInjection;
using ITFriends.Infrastructure.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Notifier.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(SharedConfigurationBuilder.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}