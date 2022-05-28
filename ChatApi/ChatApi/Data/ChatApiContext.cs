using Microsoft.EntityFrameworkCore;

namespace ChatApi.Data
{
    public class ChatApiContext : DbContext
    {
        public ChatApiContext(DbContextOptions<ChatApiContext> options)
            : base(options)
        {
        }

        public DbSet<ChatApi.User>? User { get; set; }

        public DbSet<ChatApi.Chat>? Chat { get; set; }

        public DbSet<ChatApi.UserContact>? UserContact { get; set; }

        public DbSet<ChatApi.Message>? Message { get; set; }
    }
}
