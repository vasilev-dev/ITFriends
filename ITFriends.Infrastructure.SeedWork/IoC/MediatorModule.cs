using System.Reflection;
using Autofac;
using FluentValidation;
using ITFriends.Infrastructure.SeedWork.BusinessLogicValidators;
using ITFriends.Infrastructure.SeedWork.Mediatr;
using MediatR;
using MediatR.Pipeline;

namespace ITFriends.Infrastructure.SeedWork.IoC
{
    public class MediatorModule : Autofac.Module
    {
        private readonly Assembly _assembly;

        public MediatorModule(Assembly assembly)
        {
            _assembly = assembly;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();

            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                // typeof(IRequestExceptionHandler<,,>),
                // typeof(IRequestExceptionAction<,>),
                // typeof(INotificationHandler<>)
            };
            
            foreach (var mediatorOpenType in mediatorOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(_assembly)
                    .AsClosedTypesOf(mediatorOpenType)
                    .AsImplementedInterfaces();
            }
            
            builder
                .RegisterAssemblyTypes(_assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .InstancePerLifetimeScope();
            
            builder
                .RegisterAssemblyTypes(_assembly)
                .AsClosedTypesOf(typeof(IBusinessLogicValidator<>))
                .InstancePerLifetimeScope();
            
            builder.RegisterGenericDecorator(typeof(PipelineBehavior<,>), typeof(IPipelineBehavior<,>));
            
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}