using Autofac;
using ITFriends.Topic.Core.RequestValidators;

namespace ITFriends.Topic.Api.Modules
{
    public class RequestValidatorsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new TopicMessageHtmlValidator())
                .InstancePerLifetimeScope();
            
            builder.Register(_ => new TopicMessageTitleValidator())
                .InstancePerLifetimeScope();
            
            builder.Register(_ => new TopicTitleValidator())
                .InstancePerLifetimeScope();
        }
    }
}