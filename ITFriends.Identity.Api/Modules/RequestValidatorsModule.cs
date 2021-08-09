using Autofac;
using ITFriends.Identity.Core.RequestValidators;

namespace ITFriends.Identity.Api.Modules
{
    public class RequestValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new PasswordValidator())
                .InstancePerLifetimeScope();
        }
    }
}