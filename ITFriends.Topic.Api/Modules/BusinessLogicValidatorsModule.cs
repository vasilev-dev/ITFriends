using Autofac;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Topic.Core.BusinessLogicValidators;
using ITFriends.Topic.Core.Services;

namespace ITFriends.Topic.Api.Modules
{
    public class BusinessLogicValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TopicExistsValidator>()
                .As<ITopicExistsValidator>()
                .InstancePerDependency();
            
            builder.RegisterType<TopicMessageExistsValidator>()
                .As<ITopicMessageExistsValidator>()
                .InstancePerDependency();
            
            builder.RegisterType<AppUserExistsValidator>()
                .As<IAppUserExistsValidator>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<TopicMessagePermissionValidator>()
                .As<ITopicMessagePermissionValidator>()
                .InstancePerLifetimeScope();

        }
    }
}