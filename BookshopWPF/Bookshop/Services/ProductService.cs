using Bookshop.ProductsLib;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Bookshop.Services
{
    public class ProductService
    {
        private BookService _bookService = new BookService();
        private AudioBookService _audioBookService = new AudioBookService();

        public List<Product> GetAllProducts()
        {
            var allProducts = new List<Product>();

            var allBooks = _bookService.GetAllBooks();
            var allAudioBooks = _audioBookService.GetAllAudioBooks();

            allProducts.AddRange(allBooks);
            allProducts.AddRange(allAudioBooks);

            return allProducts;
        }

        public List<Product> GetAvailableProducts()
        {
            var availableProducts = new List<Product>();

            var availableBooks = _bookService.GetAvailableBooks();
            var availableAudioBooks = _audioBookService.GetAvailableAudioBooks();

            availableProducts.AddRange(availableBooks);
            availableProducts.AddRange(availableAudioBooks);

            return availableProducts;
        }

        public List<Product> GetMissingProducts()
        {
            var missingProducts = new List<Product>();

            var missingBooks = _bookService.GetMissingBooks();
            var missingAudioBooks = _audioBookService.GetMissingAudioBooks();

            missingProducts.AddRange(missingBooks);
            missingProducts.AddRange(missingAudioBooks);

            return missingProducts;
        }

        public List<Product> GetProductsByIds(List<Guid> productIds)
        {
            var products = new List<Product>();

            var books = _bookService.GetBooksByIds(productIds);
            var audioBooks = _audioBookService.GetAudioBooksByIds(productIds);

            products.AddRange(books);
            products.AddRange(audioBooks);

            return products;
        }

        public void UpdateQuantities(Order order)
        {
            using ProductDbContext db = new ProductDbContext();

            var orderedProducts = order.OrderList.Select(x => x.Product);
            var orderedBooks = orderedProducts.OfType<Book>().ToList();
            var orderedAudioBooks = orderedProducts.OfType<AudioBook>().ToList();

            var booksToUpdate = _bookService.GetBooksByIds(orderedBooks.Select(y => y.UniqueId).ToList());
            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = order.OrderList.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity -= orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

            var audioBooksToUpdate = _audioBookService.GetAudioBooksByIds(orderedAudioBooks.Select(y => y.UniqueId).ToList());
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

            var invoicedProducts = invoice.InvoiceLines.Select(x => x.Product);
            var invoicedBooks = invoicedProducts.OfType<Book>().ToList();
            var invoicedAudioBooks = invoicedProducts.OfType<AudioBook>().ToList();

            var booksToUpdate = _bookService.GetBooksByIds(invoicedBooks.Select(y => y.UniqueId).ToList());
            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = invoice.InvoiceLines.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity += orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

            var audioBooksToUpdate = _audioBookService.GetAudioBooksByIds(invoicedAudioBooks.Select(y => y.UniqueId).ToList());
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