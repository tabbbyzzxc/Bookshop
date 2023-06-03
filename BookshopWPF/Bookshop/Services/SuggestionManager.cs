using Bookshop.ProductsLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Bookshop.Services
{
    public class SuggestionManager
    {
        public List<Product> GetRecommendedProducts(Product product)
        {
            switch (product)
            {
                case Book book:
                    {
                        return GetRecommendedBooks(book);
                    }
                case AudioBook audioBook:
                    {
                        return GetRecommendedAudioBooks(audioBook);
                    }
                default:
                    {
                        return new List<Product>();
                    }
            }
        }

        private List<Product> GetRecommendedBooks(Book book)
        {
            using var db = new ProductDbContext();
            var books = db.Books
                .Where(x => x.Genre == book.Genre && x.UniqueId != book.UniqueId)
                .Take(3)
                .ToList();
            return books.Cast<Product>().ToList();
        }

        private List<Product> GetRecommendedAudioBooks(AudioBook audioBook)
        {
            using var db = new ProductDbContext();
            var audioBooks = db.AudioBooks
                .Where(x => x.Genre == audioBook.Genre && x.UniqueId != audioBook.UniqueId)
                .Take(3)
                .ToList();
            return audioBooks.Cast<Product>().ToList();
        }
    }
}
