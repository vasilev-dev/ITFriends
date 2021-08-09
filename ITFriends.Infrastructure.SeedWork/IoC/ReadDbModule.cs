using Autofac;
using ITFriends.Infrastructure.Configuration;
using MongoDB.Driver;

namespace ITFriends.Infrastructure.SeedWork.IoC
{
    public class ReadDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(b =>
                {
                    var cfg = b.Resolve<ReadDbConfiguration>();
                    return new MongoClient(cfg.ConnectionString);
                })
                .As<IMongoClient>()
                .SingleInstance();

            builder.Register(b =>
                {
                    var client = b.Resolve<IMongoClient>();
                    var cfg = b.Resolve<ReadDbConfiguration>();
                    return client.GetDatabase(cfg.Database);
                })
                .As<IMongoDatabase>()
                .InstancePerLifetimeScope(); // TODO or use singleton ?
        }
    }
}