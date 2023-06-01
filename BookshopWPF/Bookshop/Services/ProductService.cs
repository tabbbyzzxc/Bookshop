using Bookshop.ProductsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Services
{
    public class ProductService
    {
        private BookService _bookService = new BookService();
        private AudioBookService _audioBookService = new AudioBookService();
        public List<Product> GetAllProducts(bool includeMissingProducts = false)
        {
            var _allProducts = new List<Product>();
            var allBooks = _bookService.GetAllBooks(includeMissingProducts);
            var allAudioBooks = _audioBookService.GetAllAudioBooks(includeMissingProducts);
            _allProducts.AddRange(allBooks);
            _allProducts.AddRange(allAudioBooks);
            return _allProducts;
        }

        public List<Product> GetMissingProducts()
        {
            var _missingProducts = new List<Product>();
            var allBooks = _bookService.GetMissingBooks();
            var allAudioBooks = _audioBookService.GetMissingAudioBooks();
            _missingProducts.AddRange(allBooks);
            _missingProducts.AddRange(allAudioBooks);
            return _missingProducts;
        }

        public void UpdateQuantities(Order order)
        {
            using ProductDbContext db = new ProductDbContext();
            var products = order.OrderList.Select(x => x.Product);
            var orderedBooks = products.OfType<Book>().ToList();
            var orderedAudioBooks = products.OfType<AudioBook>().ToList();
            var booksToUpdate = db.Books.Where(x => orderedBooks.Select(y => y.UniqueId).Contains(x.UniqueId)).ToList();

            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = order.OrderList.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity -= orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

            var audioBooksToUpdate = db.AudioBooks.Where(x => orderedAudioBooks.Select(y => y.UniqueId).Contains(x.UniqueId)).ToList();

            foreach (var audioBook in audioBooksToUpdate)
            {
                var orderedAudioBookQuantity = order.OrderList.First(x => x.Product.UniqueId == audioBook.UniqueId).Quantity;
                audioBook.Quantity -= orderedAudioBookQuantity;
            }

            db.UpdateRange(audioBooksToUpdate);

            db.SaveChanges();

        }

        public void UpdateQuantities(Invoice invoice)
        {
            using ProductDbContext db = new ProductDbContext();
            var products = invoice.InvoiceLines.Select(x => x.Product);
            var orderedBooks = products.OfType<Book>().ToList();
            var orderedAudioBooks = products.OfType<AudioBook>().ToList();
            var booksToUpdate = db.Books.Where(x => orderedBooks.Select(y => y.UniqueId).Contains(x.UniqueId)).ToList();

            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = invoice.InvoiceLines.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity += orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

            var audioBooksToUpdate = db.AudioBooks.Where(x => orderedAudioBooks.Select(y => y.UniqueId).Contains(x.UniqueId)).ToList();

            foreach (var audioBook in audioBooksToUpdate)
            {
                var orderedAudioBookQuantity = invoice.InvoiceLines.First(x => x.Product.UniqueId == audioBook.UniqueId).Quantity;
                audioBook.Quantity += orderedAudioBookQuantity;
            }

            db.UpdateRange(audioBooksToUpdate);

            db.SaveChanges();

        }
    }
}
