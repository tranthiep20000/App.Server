using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Data
{
    /// <summary>
    /// Information of DataContext
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; } = default!;

        /// <summary>
        /// Shops
        /// </summary>
        public DbSet<Shop> Shops { get; set; } = default!;

        /// <summary>
        /// Products
        /// </summary>
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => new { u.UserId });
            modelBuilder.Entity<Shop>().HasKey(s => new { s.ShopId });
            modelBuilder.Entity<Product>().HasKey(p => new {p.ProductId});
        }
    }
}