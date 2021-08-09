using ITFriends.Infrastructure.Data.Write.Configurations;
using ITFriends.Infrastructure.Domain.Write;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITFriends.Infrastructure.Data.Write.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicMessage> TopicMessages { get; set; }
        public DbSet<TelegramBinding> TelegramBindings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AppUserEntityTypeConfiguration().Configure(modelBuilder.Entity<AppUser>());
            new TopicEntityTypeConfiguration().Configure(modelBuilder.Entity<Topic>());
            new TopicMessageEntityTypeConfiguration().Configure(modelBuilder.Entity<TopicMessage>());
            new TelegramBindingEntityTypeConfiguration().Configure(modelBuilder.Entity<TelegramBinding>());
            
            base.OnModelCreating(modelBuilder);
        }
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}