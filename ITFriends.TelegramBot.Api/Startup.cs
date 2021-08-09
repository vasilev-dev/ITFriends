using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Infrastructure.SeedWork.IoC;
using ITFriends.TelegramBot.Api.Extensions;
using ITFriends.TelegramBot.Core;
using ITFriends.TelegramBot.Core.EventHandlers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.TelegramBot.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }
        public SharedConfiguration SharedConfiguration { get; private set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            SharedConfiguration = SharedConfigurationBuilder.BindAndValidate(Configuration);
            
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(SharedConfiguration.ConnectionStrings.WriteDbConnectionString)
                    .EnableSensitiveDataLogging();
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
            
            services.AddControllers(options =>
            {
                options.Filters.Add(new HttpResponseExceptionFilter());
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services.AddControllers(options => { options.Filters.Add(new HttpResponseExceptionFilter()); })
                .AddNewtonsoftJson();
            
            services.AddIdentity<AppUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>() // UserManager and etc use write db
                .AddDefaultTokenProviders();
            
            services.AddMassTransitHostedService();
            
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


            services.AddSwaggerGen(x => x.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo {Title = "ITFriends.TelegramBot API", Version = "v1"}));
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ToolsModule());
            builder.RegisterAutoMapper(typeof(Startup).Assembly);
            builder.RegisterTelegramBot(Configuration.GetSection("BotSettings").Get<BotConfiguration>(), 
                TelegramBotCoreProjectAssembly.Assembly);
            builder.Register(_ => SharedConfiguration);

            builder.AddMassTransit(x =>
            {
                var rabbitCfg = SharedConfiguration.RabbitMqConfiguration;

                x.AddConsumer<TopicMessageCreatedNotificationEventHandler>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitCfg.Host, rabbitCfg.Port, rabbitCfg.VHost, h =>
                    {
                        h.Username(rabbitCfg.Username);
                        h.Password(rabbitCfg.Password);
                    });

                    cfg.ReceiveEndpoint("telegram-bot", ec => { ec.ConfigureConsumer<TopicMessageCreatedNotificationEventHandler>(context); });
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("v1/swagger.json", "IT Friends"); });

            app.UseSwagger();
        }
    }
}