using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data
{
    public class BestinClassContext : DbContext
    {
        public BestinClassContext(DbContextOptions<BestinClassContext> opt) 
            : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        
        public DbSet<TestEntity> TestEntity { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}