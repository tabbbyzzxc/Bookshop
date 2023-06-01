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
                        return GetRecommendedBooks(book.Genre);
                    }
                case AudioBook audioBook:
                    {
                        return GetRecommendedAudioBooks(audioBook.Genre);
                    }
                default:
                    {
                        return new List<Product>();
                    }
            }
        }

        private List<Product> GetRecommendedBooks(string genre)
        {
            using var db = new ProductDbContext();
            var books = db.Books.Where(x => x.Genre == genre).Take(3).ToList();
            return books.Cast<Product>().ToList();
        }

        private List<Product> GetRecommendedAudioBooks(string genre)
        {
            using var db = new ProductDbContext();
            var audioBooks = db.AudioBooks.Where(x => x.Genre == genre).Take(3).ToList();
            return audioBooks.Cast<Product>().ToList();
        }
    }
}
