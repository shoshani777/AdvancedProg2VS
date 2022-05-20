using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdvancedProgramming2Server.Models;

namespace AdvancedProgramming2Server.Data
{
    public class AdvancedProgramming2ServerContext : DbContext
    {
        public AdvancedProgramming2ServerContext (DbContextOptions<AdvancedProgramming2ServerContext> options)
            : base(options)
        {
        }

        public DbSet<AdvancedProgramming2Server.Models.User>? User { get; set; }

        public DbSet<AdvancedProgramming2Server.Models.Chat>? Chat { get; set; }

        public DbSet<AdvancedProgramming2Server.Models.Message>? Message { get; set; }
    }
}
