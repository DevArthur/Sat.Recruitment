using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Persistence.DataSeeding;
using Sat.Recruitment.Persistence.Models;

namespace Sat.Recruitment.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Data seeding
            DatabaseData.Seed(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
