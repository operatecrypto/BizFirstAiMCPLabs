using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace YourNamespace.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // OPTIONAL: OnModelCreating for custom table mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed sample data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 799.99m },
                new Product { Id = 2, Name = "Smartphone", Price = 499.99m },
                new Product { Id = 3, Name = "Headphones", Price = 99.99m }
            );
        }
    }
}
