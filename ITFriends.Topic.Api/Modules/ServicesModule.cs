using Autofac;
using ITFriends.Topic.Core.Services;

namespace ITFriends.Topic.Api.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Base64Converter>()
                .As<IBase64Converter>()
                .InstancePerDependency();
        }
    }
}