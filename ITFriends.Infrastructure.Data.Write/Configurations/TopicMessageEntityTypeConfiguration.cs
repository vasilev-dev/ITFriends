using ITFriends.Infrastructure.Domain.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITFriends.Infrastructure.Data.Write.Configurations
{
    public class TopicMessageEntityTypeConfiguration : IEntityTypeConfiguration<TopicMessage>
    {
        public void Configure(EntityTypeBuilder<TopicMessage> builder)
        {
            builder
                .Property(m => m.Title)
                .IsRequired();

            builder
                .Property(m => m.CreatedAt)
                .IsRequired();
            
            builder
                .Property(m => m.HtmlBase64)
                .IsRequired();

            builder
                .HasOne(m => m.Topic)
                .WithMany(t => t.TopicMessages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.TopicId);
            
            builder
                .HasOne(m => m.Author)
                .WithMany(u => u.TopicMessages)
                .HasForeignKey(m => m.AuthorAppUserId);
        }
    }
}