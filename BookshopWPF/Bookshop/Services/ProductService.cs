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
        public List<Product> GetAllProducts(bool includeMissingProducts = false)
        {
            var _allProducts = new List<Product>();
            BookService _bookService = new BookService();
            AudioBookService _audioBookService = new AudioBookService();
            var allBooks = _bookService.GetAllBooks(includeMissingProducts);
            var allAudioBooks = _audioBookService.GetAllAudioBooks(includeMissingProducts);
            _allProducts.AddRange(allBooks);
            _allProducts.AddRange(allAudioBooks);
            return _allProducts;
        }

        public List<Product> GetMissingProducts()
        {
            var _missingProducts = new List<Product>();
            BookService _bookService = new BookService();
            AudioBookService _audioBookService = new AudioBookService();
            var allBooks = _bookService.GetMissingBooks();
            var allAudioBooks = _audioBookService.GetMissingAudioBooks();
            _missingProducts.AddRange(allBooks);
            _missingProducts.AddRange(allAudioBooks);
            return _missingProducts;
        }

    }
}
