using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming2Server.Data
{
    public class AdvancedProgramming2ServerContext : DbContext
    {
        public AdvancedProgramming2ServerContext(DbContextOptions<AdvancedProgramming2ServerContext> options)
            : base(options)
        {
        }


        public DbSet<AdvancedProgramming2Server.Models.Rating>? Rating { get; set; }
    }
}
