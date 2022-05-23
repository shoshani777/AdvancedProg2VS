using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApi;

namespace ChatApi.Data
{
    public class ChatApiContext : DbContext
    {
        public ChatApiContext (DbContextOptions<ChatApiContext> options)
            : base(options)
        {
        }

        public DbSet<ChatApi.User>? User { get; set; }

        public DbSet<ChatApi.Chat>? Chat { get; set; }

        public DbSet<ChatApi.UserContact>? UserContact { get; set; }
    }
}
