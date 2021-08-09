using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ITFriends.Identity.Api.Modules;
using ITFriends.Identity.Core;
using ITFriends.Identity.Core.EventHandlers;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Infrastructure.SeedWork.IoC;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public SharedConfiguration SharedConfiguration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            SharedConfiguration = SharedConfigurationBuilder.BindAndValidate(Configuration);

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(SharedConfiguration.ConnectionStrings.WriteDbConnectionString);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddIdentity<AppUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>() // UserManager and etc use write db
                .AddDefaultTokenProviders();
            
            services.AddControllers(options =>
            {
                options.Filters.Add(new HttpResponseExceptionFilter());
            });
            
            services.AddAuthentication(options => 
                {    
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = SharedConfiguration.IS4Url;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = Configuration["IS4Scope"];
                });

            services.AddMassTransitHostedService();
            
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ITFriends.Identity API", Version = "v1" }));
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule(IdentityCoreProjectAssembly.Assembly));
            builder.RegisterModule(new ReadDbModule());
            builder.RegisterModule(new ReadDbRepositoriesModule());
            builder.RegisterModule(new ToolsModule());
            builder.RegisterModule(new RequestValidatorsModule());
            builder.RegisterModule(new BusinessLogicValidatorsModule());
            builder.RegisterAutoMapper(typeof(Startup).Assembly);
            
            builder.Register(_ => SharedConfiguration);
            builder.Register(_ => SharedConfiguration.ReadDbConfiguration);
            builder.Register(_ => Configuration.GetSection("InvitationEmailConfiguration")
                .Get<InvitationEmailConfiguration>());
            builder.Register(_ => Configuration.GetSection("RestorePasswordEmailConfiguration")
                .Get<RestorePasswordEmailConfiguration>());
            
            builder.AddMassTransit(x =>
            {
                var rabbitCfg = SharedConfiguration.RabbitMqConfiguration;
                
                x.AddConsumer<UserCreatedDbSynchronizer>();
                x.AddConsumer<UserEmailConfirmedEventHandler>();
                x.AddConsumer<TelegramBindingConfirmedDbSynchronizer>();
                x.AddConsumer<ChangeRoleDbSynchronizer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitCfg.Host, rabbitCfg.Port,rabbitCfg.VHost, h =>
                    {
                        h.Username(rabbitCfg.Username);
                        h.Password(rabbitCfg.Password);
                    });
                    
                    cfg.ReceiveEndpoint("db_sync_identity", ec =>
                    {
                        ec.ConfigureConsumer<UserCreatedDbSynchronizer>(context);
                        ec.ConfigureConsumer<UserEmailConfirmedEventHandler>(context);
                        ec.ConfigureConsumer<TelegramBindingConfirmedDbSynchronizer>(context);
                        ec.ConfigureConsumer<ChangeRoleDbSynchronizer>(context);
                    });
                });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("v1/swagger.json", "IT Friends");
            });

            app.UseSwagger();
        }
    }
}
