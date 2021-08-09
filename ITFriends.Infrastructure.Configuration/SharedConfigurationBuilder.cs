using System.IO;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ITFriends.Infrastructure.Configuration
{
    public static class SharedConfigurationBuilder
    {
        public static void Configure(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            var env = hostBuilderContext.HostingEnvironment;
            var sharedFolder = Path.Combine(env.ContentRootPath, "..", "ITFriends.Infrastructure.Configuration");

            configurationBuilder
                .AddJsonFile(Path.Combine(sharedFolder, "SharedConfiguration.json"), true)                    
                .AddJsonFile(Path.Combine(sharedFolder, $"SharedConfiguration.{env.EnvironmentName}.json"), true)
                .AddJsonFile("SharedConfiguration.json", true)
                .AddJsonFile($"SharedConfiguration.{env.EnvironmentName}.json", true)                      
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            configurationBuilder.AddEnvironmentVariables();
        }

        public static SharedConfiguration BindAndValidate(IConfiguration configuration)
        {
            var sharedConfiguration = new SharedConfiguration();
            configuration.Bind(sharedConfiguration);

            var validator = new SharedConfigurationValidator();
            validator.ValidateAndThrow(sharedConfiguration);

            return sharedConfiguration;
        }
    }
}