using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BestinClass.Infrastructure.Data
{
    public class BestinClassContext : DbContext
    {
        public BestinClassContext(DbContextOptions<BestinClassContext> opt) 
            : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Review>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reviews)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Car)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Reviews)
                .WithOne();
                */
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Car)
                .OnDelete(DeleteBehavior.SetNull);
        }
        
        public DbSet<TestEntity> TestEntity { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}