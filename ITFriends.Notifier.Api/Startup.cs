using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ITFriends.Infrastructure.Configuration;
using ITFriends.Infrastructure.Data.Write.Contexts;
using ITFriends.Infrastructure.SeedWork.Errors;
using ITFriends.Infrastructure.SeedWork.IoC;
using ITFriends.Notifier.Core;
using ITFriends.Notifier.Core.EventHandlers;
using ITFriends.Notifier.Core.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Notifier.Api
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
                { config.UseSqlServer(SharedConfiguration.ConnectionStrings.WriteDbConnectionString); });

            services.AddControllers(options => { options.Filters.Add(new HttpResponseExceptionFilter()); });
            
            services.AddMassTransitHostedService();

            services.AddSwaggerGen(x => x.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo {Title = "ITFriends.Notifier API", Version = "v1"}));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule(NotifierCoreProjectAssembly.Assembly));
            builder.RegisterModule(new ToolsModule());
            builder.RegisterAutoMapper(typeof(Startup).Assembly);
            builder.Register(_ => SharedConfiguration.ReadDbConfiguration);
            builder.Register(_ => Configuration.GetSection("SmtpClient").Get<SmtpClientConfiguration>());
            builder.RegisterType<SmtpEmailSender>().As<IEmailSender>();

            builder.AddMassTransit(x =>
            {
                var rabbitCfg = SharedConfiguration.RabbitMqConfiguration;

                x.AddConsumer<SendEmailEventHandler>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitCfg.Host, rabbitCfg.Port, rabbitCfg.VHost, h =>
                    {
                        h.Username(rabbitCfg.Username);
                        h.Password(rabbitCfg.Password);
                    });

                    cfg.ReceiveEndpoint("notifier", ec => { ec.ConfigureConsumer<SendEmailEventHandler>(context); });
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerUI(x => { x.SwaggerEndpoint("v1/swagger.json", "IT Friends"); });

            app.UseSwagger();
        }
    }
}