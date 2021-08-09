using Autofac;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;

namespace ITFriends.Identity.Api.Modules
{
    public class BusinessLogicValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserExistsValidator>()
                .As<IAppUserExistsValidator>()
                .InstancePerLifetimeScope();
        }
    }
}