using ITFriends.Infrastructure.Domain.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITFriends.Infrastructure.Data.Write.Configurations
{
    public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .Property(u => u.FirstName)
                .IsRequired();

            builder
                .Property(u => u.LastName)
                .IsRequired();

            builder
                .HasMany(u => u.Subscriptions)
                .WithMany(t => t.Subscribers)
                .UsingEntity(j => j.ToTable("AppUserSubscription"));
        }
    }
}