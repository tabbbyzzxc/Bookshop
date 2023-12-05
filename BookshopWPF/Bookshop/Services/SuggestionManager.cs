using Bookshop.ProductsLib;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Bookshop.Services
{
    public class SuggestionManager
    {
        public List<Book> GetRecommendedProducts(Book product)
        {
            switch (product)
            {
                case Book book:
                    {
                        return GetRecommendedBooks(book);
                    }
                default:
                    {
                        return new List<Book>();
                    }
            }
        }

        private List<Book> GetRecommendedBooks(Book book)
        {
            using var db = new ProductDbContext();
            var books = db.Books
                .Where(x => x.Genre == book.Genre && x.UniqueId != book.UniqueId)
                .Take(3)
                .ToList();
            return books.Cast<Book>().ToList();
        }
    }
}
