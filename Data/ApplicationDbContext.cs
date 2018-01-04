using Microsoft.EntityFrameworkCore;
using toolservice.Model;

namespace toolservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToolType> ToolTypes { get; set; }
        public DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tool>()
                .HasIndex(b => b.serialNumber);
            modelBuilder.Entity<Tool>()
            .Property(b => b.status)
            .HasDefaultValue("available");
        }
    }
}