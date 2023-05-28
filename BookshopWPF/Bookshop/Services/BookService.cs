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
        public List<Book> GetAllBooks()
        {
            using var db = new ProductDbContext();
            return db.Books.Where(x => x.Quantity > 0).ToList();
        }

        public bool AddBook(Book book)
        {
            using var db = new ProductDbContext();
            var exist = db.Books.FirstOrDefault(x => x.Title == book.Title && x.Author == book.Author && x.PaperType == book.PaperType && x.PageQuantity == book.PageQuantity);
            if (exist != null)
            {
                return false;
            }

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
