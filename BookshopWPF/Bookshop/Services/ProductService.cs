using Bookshop.ProductsLib;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Bookshop.Services
{
    public class ProductService
    {
        private BookService _bookService = new BookService();
        
        public List<Book> GetAllProducts()
        {
            var allProducts = new List<Book>();

            var allBooks = _bookService.GetAllBooks();
            

            allProducts.AddRange(allBooks);
            

            return allProducts;
        }

        public List<Book> GetAvailableProducts()
        {
            var availableProducts = new List<Book>();

            var availableBooks = _bookService.GetAvailableBooks();
           

            availableProducts.AddRange(availableBooks);
            

            return availableProducts;
        }

        public List<Book> GetMissingProducts()
        {
            var missingProducts = new List<Book>();

            var missingBooks = _bookService.GetMissingBooks();
            

            missingProducts.AddRange(missingBooks);
            

            return missingProducts;
        }

        public List<Book> GetProductsByIds(List<Guid> productIds)
        {
            var products = new List<Book>();

            var books = _bookService.GetBooksByIds(productIds);
            

            products.AddRange(books);
            

            return products;
        }

        public void UpdateQuantities(Order order)
        {
            using ProductDbContext db = new ProductDbContext();

            var orderedProducts = order.OrderList.Select(x => x.Product);
            var orderedBooks = orderedProducts.OfType<Book>().ToList();
            

            var booksToUpdate = _bookService.GetBooksByIds(orderedBooks.Select(y => y.UniqueId).ToList());
            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = order.OrderList.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity -= orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

           
            db.SaveChanges();
        }

        public void UpdateQuantities(Invoice invoice)
        {
            using ProductDbContext db = new ProductDbContext();

            var invoicedProducts = invoice.InvoiceLines.Select(x => x.Product);
            var invoicedBooks = invoicedProducts.OfType<Book>().ToList();
           

            var booksToUpdate = _bookService.GetBooksByIds(invoicedBooks.Select(y => y.UniqueId).ToList());
            foreach (var book in booksToUpdate)
            {
                var orderedBookQuantity = invoice.InvoiceLines.First(x => x.Product.UniqueId == book.UniqueId).Quantity;
                book.Quantity += orderedBookQuantity;
            }

            db.UpdateRange(booksToUpdate);

            db.SaveChanges();
        }
    }
}