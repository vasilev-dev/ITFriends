using Autofac;
using ITFriends.Identity.Data.Read.Repositories;

namespace ITFriends.Identity.Api.Modules
{
    public class ReadDbRepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepository>()
                .As<IAppUserRepository>()
                .InstancePerLifetimeScope();
        }
    }
}