using ITFriends.Infrastructure.Domain.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITFriends.Infrastructure.Data.Write.Configurations
{
    public class TopicEntityTypeConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder
                .Property(t => t.Title)
                .IsRequired();

            builder
                .HasOne(t => t.ParentTopic)
                .WithMany(t => t.SubTopics)
                .HasForeignKey(t => t.ParentTopicId);
        }
    }
}