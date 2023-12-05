using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Documents;
using System.Windows.Media;

namespace Bookshop
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {
            var dbExists = Database.EnsureCreated();
            if (dbExists)
            {
                GenerateRandomProducts();
            }
            
        }

        private void GenerateRandomProducts()
        {
            string[] languages = { "Ukrainian", "English", "Spanish", "French", "German" };
            string json = Bookshop.Properties.Resources.JSONTEXT;
            var random = new Random();
            List<Book> list = JsonSerializer.Deserialize<List<Book>>(json);

           

           

            var books = list.Skip(20).Take(list.Count - 20).Select(x => new Book
            {
                Title= x.Title,
                Author= x.Author,
                Description = x.Description,
                Genre = x.Genre,
                PageQuantity = random.Next(100, 500),
                PaperType = AppConstants.PaperTypes[random.Next(AppConstants.PaperTypes.Length)],
                BuyPrice = random.Next(50, 120),
                SellPrice = random.Next(120, 200),
                Quantity = random.Next(1, 200),
                Language = languages[random.Next(languages.Length)],
                UniqueId = Guid.NewGuid()
            });

            Books.AddRange(books);

            SaveChanges();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>().HasOne(o => o.Order).WithMany(o => o.OrderList).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<InvoiceLine>().HasOne(o => o.Invoice).WithMany(o => o.InvoiceLines).HasForeignKey(o => o.InvoiceId);
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               Properties.Settings.Default.ConnectionString);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
