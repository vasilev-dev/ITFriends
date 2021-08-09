using Autofac;
using ITFriends.Infrastructure.Data.Repositories.Read;
using ITFriends.Infrastructure.Domain.Read;
using ITFriends.Topic.Data.Read.Repositories;

namespace ITFriends.Topic.Api.Modules
{
    public class ReadDbRepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TopicRepository>()
                .As<ITopicRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<TopicMessageRepository>()
                .As<ITopicMessageRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<AppUserRepository>()
                .As<IAppUserRepository>()
                .InstancePerLifetimeScope();
        }
    }
}