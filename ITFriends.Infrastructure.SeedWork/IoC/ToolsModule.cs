using Autofac;
using Serilog;

namespace ITFriends.Infrastructure.SeedWork.IoC
{
    public class ToolsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("Logs\\log_.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Console()
                    .CreateLogger())
                .As<ILogger>();
        }
    }
}