using API_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Task.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c=>c.Name).IsRequired().HasMaxLength(70);
            base.OnModelCreating(modelBuilder);
        }
    }
}
