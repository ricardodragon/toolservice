using Microsoft.EntityFrameworkCore;
using toolservice.Model;

namespace toolservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ToolType> ToolTypes{get;set;}
        public DbSet<Tool> Tools{get;set;}

        
    }
}