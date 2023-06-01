using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System;

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

            var book = new Book
            {
                Author = "Дженніфер Л. Арментраут",
                Title = "Війна двох королев",
                BuyPrice = 1000,
                Description = "Маківка стала не лише королевою, а й богом, проте це її геть не тішить. Хіба може вона почуватися щасливою, коли Кастіл, її споріднене серце, є бранцем Кривавої королеви? Дівчина ладна на все, аби звільнити коханого, але для цього їй доведеться переконати військову еліту Атлантії вести війну за її правилами та знайти рівновагу між особистим життям і вищою метою — звільненням Солісу від тиранії Кривавої королеви.",
                Genre = AppConstants.Genres[2],
                Language = "Ukrainian",
                PageQuantity = 100,
                PaperType = AppConstants.PaperTypes[0],
                SellPrice = 1500,
                Quantity = 100,
                UniqueId = Guid.NewGuid()
            };
            var audio = new AudioBook
            {
                Author = "Ерін Гантер",
                Title = "Нове пророцтво",
                BuyPrice = 900,
                Description = "«Коты-воины» — мировой бестселлер, серия приключенческих романов-фэнтези для детей и подростков. Первая книга серии была написана в 2001 году британскими писательницами под общим псевдонимом Эрин Гантер (Кейт Кэри, Черит Болдри, Тай Сазерленд, Виктория Голмс). В Украине книги серии «Коты-воины» появились в 2016 году.",
                Genre = AppConstants.Genres[1],
                Language = "Ukrainian",
                Format = AppConstants.Formats[0],
                SellPrice = 1200,
                Quantity = 20,
                UniqueId = Guid.NewGuid()
            };
            Books.Add(book);
            AudioBooks.Add(audio);
            SaveChanges();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<AudioBook> AudioBooks { get; set; }

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
