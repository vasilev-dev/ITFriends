using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITFriends.Identity.IdentityServer
{
    // ReSharper disable once InconsistentNaming
    public static class IS4DbInitializer
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                var clients = Config.Clients.Select(x => x.ToEntity());
                context.Clients.AddRange(clients);
                context.SaveChanges();
            }
        
            if (!context.IdentityResources.Any())
            {
                var identityResources = Config.IdentityResources.Select(x => x.ToEntity());
                context.IdentityResources.AddRange(identityResources);
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                var apiResources = Config.ApiResources.Select(x => x.ToEntity());
                context.ApiResources.AddRange(apiResources);
                context.SaveChanges();
            }
        
            // ReSharper disable once InvertIf
            if (!context.ApiScopes.Any())
            {
                var scopes = Config.ApiScopes.Select(x => x.ToEntity());
                context.ApiScopes.AddRange(scopes);
                context.SaveChanges();
            }
        }
    }
}