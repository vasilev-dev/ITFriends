using Autofac.Extensions.DependencyInjection;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Read.Migrations;
using ITFriends.Infrastructure.Data.Write.Migrations.WriteDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Topic.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            
            WriteDbMigrator.RunMigrate(services);
            ReadDbMigrator.RunMigrate(services);
            
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