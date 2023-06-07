using Microsoft.EntityFrameworkCore;
using TennisApp.Models;

namespace TennisApp.Data
{
    public class TennisContext : DbContext
    {
        public TennisContext(DbContextOptions<TennisContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().HasKey(l => l.Name);
        }

        public DbSet<Lesson> Lessons { get; set; }
    }
}
