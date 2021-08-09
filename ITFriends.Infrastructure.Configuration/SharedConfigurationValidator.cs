using FluentValidation;

namespace ITFriends.Infrastructure.Configuration
{
    public class SharedConfigurationValidator : AbstractValidator<SharedConfiguration>
    {
        public SharedConfigurationValidator()
        {
            RuleFor(x => x.ClientUrl).NotNull();
            
            RuleFor(x => x.IS4Url).NotNull();

            RuleFor(x => x.ConnectionStrings).NotNull();
            RuleFor(x => x.ConnectionStrings)
                .SetValidator(new ConnectionStringsConfigurationValidator());

            RuleFor(x => x.ReadDbConfiguration).NotNull();
            RuleFor(x => x.ReadDbConfiguration)
                .SetValidator(new ReadDbConfigurationValidator());

            RuleFor(x => x.RabbitMqConfiguration).NotNull();
            RuleFor(x => x.RabbitMqConfiguration)
                .SetValidator(new RabbitMqConfigurationValidator());
        }
    }

    public sealed class ConnectionStringsConfigurationValidator : AbstractValidator<ConnectionStringsConfiguration>
    {
        public ConnectionStringsConfigurationValidator()
        {
            RuleFor(x => x.WriteDbConnectionString).NotNull();
        }
    }

    public sealed class ReadDbConfigurationValidator : AbstractValidator<ReadDbConfiguration>
    {
        public ReadDbConfigurationValidator()
        {
            RuleFor(x => x.ConnectionString).NotNull();
            RuleFor(x => x.Database).NotNull();
        }
    }

    public sealed class RabbitMqConfigurationValidator : AbstractValidator<RabbitMQConfiguration>
    {
        public RabbitMqConfigurationValidator()
        {
            RuleFor(x => x.Host).NotNull();
            RuleFor(x => x.VHost).NotNull();
            RuleFor(x => x.Port).NotNull();
            RuleFor(x => x.Username).NotNull();
            RuleFor(x => x.Password).NotNull();
        }
    }
}