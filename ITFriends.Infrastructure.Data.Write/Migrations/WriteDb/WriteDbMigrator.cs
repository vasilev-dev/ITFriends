using System;
using ITFriends.Infrastructure.Data.Write.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ITFriends.Infrastructure.Data.Write.Migrations.WriteDb
{
    public static class WriteDbMigrator
    {
        public static void RunMigrate(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger>();
                logger.Fatal(ex, "An error occurred creating the write DB");
            }
        }
    }
}