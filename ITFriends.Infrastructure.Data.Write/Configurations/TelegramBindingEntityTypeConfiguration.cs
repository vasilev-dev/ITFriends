using ITFriends.Infrastructure.Domain.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITFriends.Infrastructure.Data.Write.Configurations
{
    public class TelegramBindingEntityTypeConfiguration : IEntityTypeConfiguration<TelegramBinding>
    {
        public void Configure(EntityTypeBuilder<TelegramBinding> builder)
        {
            builder.HasKey(b => new {b.ChatId, b.AppUserId});
            
            builder
                .HasOne(b => b.AppUser)
                .WithOne(u => u.TelegramBinding);

            builder
                .Property(b => b.TelegramUserName)
                .IsRequired();

            builder
                .Property(b => b.LanguageCode)
                .HasDefaultValue("en")
                .IsRequired();

            builder
                .Property(b => b.IsConfirmed)
                .IsRequired();

            builder.ToTable("TelegramBindings"); // EF for some reason gives the table a singular name
        }
    }
}