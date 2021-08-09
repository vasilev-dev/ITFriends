using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.Domain.Write;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Infrastructure.SeedWork.IoC;
using ITFriends.Topic.Api.Modules;
using ITFriends.Topic.Core;
using ITFriends.Topic.Core.EventHandlers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Topic.Api
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
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
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
            
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ITFriends.Topic API", Version = "v1" }));
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule(TopicCoreProjectAssembly.Assembly));
            builder.RegisterModule(new ReadDbModule());
            builder.RegisterModule(new ReadDbRepositoriesModule());
            builder.RegisterModule(new ToolsModule());
            builder.RegisterModule(new RequestValidatorsModule());
            builder.RegisterModule(new BusinessLogicValidatorsModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterAutoMapper(typeof(Startup).Assembly);
            builder.Register(_ => SharedConfiguration);
            builder.Register(_ => SharedConfiguration.ReadDbConfiguration);

            builder.AddMassTransit(x =>
            {
                var rabbitCfg = SharedConfiguration.RabbitMqConfiguration;
                
                x.AddConsumer<TopicCreatedDbSynchronizer>();
                x.AddConsumer<TopicEditedDbSynchronizer>();
                x.AddConsumer<TopicDeletedDbSynchronizer>();
                x.AddConsumer<TopicMessageCreatedDbSynchronizer>();
                x.AddConsumer<TopicMessageHtmlEditedDbSynchronizer>();
                x.AddConsumer<TopicMessageTitleEditedDbSynchronizer>();
                x.AddConsumer<TopicMessageDeletedDbSynchronizer>();
                x.AddConsumer<SubscribeToTopicDbSynchronizer>();
                x.AddConsumer<UnsubscribeFromTopicDbSynchronizer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitCfg.Host, rabbitCfg.Port,rabbitCfg.VHost, h =>
                    {
                        h.Username(rabbitCfg.Username);
                        h.Password(rabbitCfg.Password);
                    });
                    
                    cfg.ReceiveEndpoint("db_sync_topic", ec =>
                    {
                        ec.ConfigureConsumer<TopicCreatedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicEditedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicDeletedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicMessageCreatedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicMessageHtmlEditedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicMessageTitleEditedDbSynchronizer>(context);
                        ec.ConfigureConsumer<TopicMessageDeletedDbSynchronizer>(context);
                        ec.ConfigureConsumer<SubscribeToTopicDbSynchronizer>(context);
                        ec.ConfigureConsumer<UnsubscribeFromTopicDbSynchronizer>(context);
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

            app.UseHttpsRedirection();

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