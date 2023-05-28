using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bookshop
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<AudioBook> AudioBooks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>().HasOne(o => o.Order).WithMany(o => o.OrderList).HasForeignKey(o => o.OrderId);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               "Server=localhost;Port=5432;User Id=postgres;Password=159874;Database=ProductDb;");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
