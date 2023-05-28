using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new ProductDbContext();
            var books = db.Books.ToList();
        }
    }

    public class ProductDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=ProductDb;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}