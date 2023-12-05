using Bookshop.ProductsLib;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class BookService
    {
        public List<Book> GetAllBooks()
        {
            using var db = new ProductDbContext();
            return db.Books.ToList();
        }

        public List<Book> GetAvailableBooks()
        {
            using var db = new ProductDbContext();
            return db.Books.Where(x => x.Quantity > 0).ToList();
        }

        public List<Book> GetMissingBooks()
        {
            using var db = new ProductDbContext();
            return db.Books.Where(x => x.Quantity == 0).ToList();
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
            item.SellPrice = book.SellPrice;
            item.BuyPrice = book.BuyPrice;
            item.Title = book.Title;
            item.Author = book.Author;
            item.PaperType = book.PaperType;
            item.PageQuantity = book.PageQuantity;
            item.Genre = book.Genre;
            item.Language = book.Language;
            db.Update(item);
            db.SaveChanges();

        }

        public IEnumerable<Book> GetBooksByIds(List<Guid> productIds)
        {
            using var db = new ProductDbContext();
            return db.Books.Where(x => productIds.Contains(x.UniqueId)).ToList();
        }
    }
}