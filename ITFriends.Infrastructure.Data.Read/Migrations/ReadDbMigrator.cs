using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace ITFriends.Infrastructure.Data.Read.Migrations
{
    public static class ReadDbMigrator
    {
        public static void RunMigrate(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<IMongoDatabase>();
                ReadDbMigrations.ExecuteMigrations(context);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger>();
                logger.Fatal(ex, "An error occurred creating the read DB");
            }
        }
    }
}