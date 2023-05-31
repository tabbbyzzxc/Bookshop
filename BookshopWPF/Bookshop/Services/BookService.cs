using Bookshop.ProductsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookshop.Services
{
    public class BookService
    {

        public List<Book> GetAllBooks(bool includeMissingBooks)
        {
            using var db = new ProductDbContext();
            var books = db.Books.AsQueryable();
            if (!includeMissingBooks)
            {
                books = books.Where(x => x.Quantity > 0);
                
            }

            return books.ToList();
        }

        public bool AddBook(Book book)
        {
            using var db = new ProductDbContext();
            var exist = db.Books.FirstOrDefault(x => x.Title == book.Title && x.Author == book.Author && x.PaperType == book.PaperType && x.PageQuantity == book.PageQuantity);
            if (exist != null)
            {
                return false;
            }

            book.UniqueId = Guid.NewGuid();
            db.Books.Add(book);
            db.SaveChanges();

            return true;
        }

        public void UpdateBook(Book book)
        {
            using var db = new ProductDbContext();
            var item = db.Books.FirstOrDefault(x => x.Id == book.Id);
            if (item == null)
            {
                return;
            }
            item.Description = book.Description;
            item.SellPrice =book.SellPrice;
            item.BuyPrice =book.BuyPrice;
            item.Title = book.Title;
            item.Author = book.Author;
            item.PaperType = book.PaperType;
            item.PageQuantity = book.PageQuantity;
            item.Genre = book.Genre;
            item.Language = book.Language;
            db.Update(item);
            db.SaveChanges();
            
        }

        public List<Book> GetMissingBooks()
        {
            using var db = new ProductDbContext();
            return db.Books.Where(x => x.Quantity == 0).ToList();
        }
    }
}
