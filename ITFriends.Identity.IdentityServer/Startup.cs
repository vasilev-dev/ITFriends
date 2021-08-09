using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Identity.IdentityServer
{
     public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public SharedConfiguration SharedConfiguration { get; private set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            SharedConfiguration = SharedConfigurationBuilder.BindAndValidate(Configuration);
            
            var migrationsAssembly = WriteDataProjectAssembly.Assembly;
            var writeDbConnectionString = SharedConfiguration.ConnectionStrings.WriteDbConnectionString;

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(writeDbConnectionString, 
                    x => x.MigrationsAssembly(migrationsAssembly));
            });
            
            services.AddIdentity<AppUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<AppUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(writeDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(writeDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    // options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                })
                .AddProfileService<ProfileService>(); // add user claims to is4

            builder.AddDeveloperSigningCredential();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IS4DbInitializer.InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseIdentityServer();
        }
    }
}