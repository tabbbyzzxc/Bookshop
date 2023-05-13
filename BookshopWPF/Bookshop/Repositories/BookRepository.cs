using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using Bookshop.Models;
using Bookshop.ViewModels;

namespace Bookshop.Repositories
{


    public class BookRepository
    {
        private static string _path = "Database";
        private static string _pathBooksDB = Path.Combine(_path, "dbFile.txt");
        private static string _pathQuantityDB = Path.Combine(_path, "bookQuantity.txt");


        public BookRepository()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            if (!File.Exists(_pathBooksDB))
            {
                File.Create(_pathBooksDB);
            }
            if (!File.Exists(_pathQuantityDB))
            {
                File.Create(_pathQuantityDB);
            }
        }
        public bool AddBook(Book book)
        {
            var bookList = GetAllBooks();
            var exist = bookList.FirstOrDefault(x => x.Author == book.Author && x.Name == book.Name);
            if(exist != null)
            {
                return false;
            }

            book.Id = GetNewId(bookList);
            bookList.Add(book);

            string serializedList = JsonSerializer.Serialize(bookList);
            File.WriteAllText(_pathBooksDB, serializedList);
            return true;
        }

        public List<Book> GetAllBooks()
        {
            var content = File.ReadAllText(_pathBooksDB);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Book>();
            }

            var bookList = JsonSerializer.Deserialize<List<Book>>(content);
            return bookList;
        }


        public void UpdateBook(Book book)
        {
            var bookList = GetAllBooks();
            var item = bookList.FirstOrDefault(x => x.Id == book.Id);
            if (item == null)
            {
                return;
            }

            item.Author = book.Author;
            item.Name = book.Name;
            item.BuyPrice = book.BuyPrice;
            item.SellPrice = book.SellPrice;
            
            string serializedList = JsonSerializer.Serialize(bookList);
            File.WriteAllText(_pathBooksDB, serializedList);
        }

        public List<Book> GetMissingBooks()
        {
            var allBooks = GetAllBooks();
            var bookQuantities = GetBooksQuantities();
            var missingBooksIds = bookQuantities.Where(x => x.Quantity == 0).Select(x => x.BookId).ToList();
            var missingBooks = allBooks.Where(x => missingBooksIds.Contains(x.Id)).ToList();
            return missingBooks;
        }

     
        public void AddQuantity(List<BookQuantity> arrivalList)
        {

            var existingQuantities = GetBooksQuantities();
            foreach (var book in arrivalList)
            {
                var exist = existingQuantities.FirstOrDefault(x => x.BookId == book.BookId);
                if(exist != null)
                {
                    exist.Quantity += book.Quantity;
                }
                else
                {
                    existingQuantities.Add(book);
                }
            }

            string serializedList = JsonSerializer.Serialize(existingQuantities);
            File.WriteAllText(_pathQuantityDB, serializedList);
        }

        public List<BookQuantity> GetBooksQuantities()
        {
            var content = File.ReadAllText(_pathQuantityDB);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<BookQuantity>();
            }

            var bookIdList = JsonSerializer.Deserialize<List<BookQuantity>>(content);
            return bookIdList;
        }

        public List<BookAvailableModel> GetAvailableBooks()
        {
            
            var allBooks = GetAllBooks();
            var booksQuantities = GetBooksQuantities();       
            var availableQuantitiesIds = booksQuantities.Where(x => x.Quantity > 0).Select(x => x.BookId).ToList();
            var availableBooks = allBooks.Where(x => availableQuantitiesIds.Contains(x.Id)).Select(x => new BookAvailableModel()
            {
                Id = x.Id,
                Author = x.Author,
                Name = x.Name,
                SellPrice = x.SellPrice,
                Quantity = booksQuantities.First(y => y.BookId == x.Id).Quantity 
            }).ToList();
            return availableBooks;
        }

        public void UpdateBookQuantity(Order order)
        {
            var booksQuantity = GetBooksQuantities();
            var orderList = order.OrderList;
            foreach (var item in orderList)
            {
                var book = booksQuantity.First(x => x.BookId == item.Id);
                book.Quantity -= item.Quantity;
            }

            string serializedList = JsonSerializer.Serialize(booksQuantity);
            File.WriteAllText(_pathQuantityDB, serializedList);
        }

        private long GetNewId(List<Book> list)
        {
            if (!list.Any())
            {
                return 1;
            }
            return list.Max(x => x.Id) + 1;
        }
    }
}
