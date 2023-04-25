using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bookshop.Models;

namespace Bookshop.Repositories
{


    internal class BookRepository
    {
        private static string _path = "Database";
        private static string _pathFile = Path.Combine(_path, "dbFile.txt");

        public BookRepository()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            if (!File.Exists(_pathFile))
            {
                File.Create(_pathFile);
            }
        }
        public bool AddBook(Book book)
        {

            var bookList = GetAllBooks();

            foreach (var item in bookList)
            {
                if (item.Author == book.Author && item.Name == book.Name)
                {
                    return false;
                }
            }


            book.Id = GetNewId(bookList);
            bookList.Add(book);
            string serializedList = JsonSerializer.Serialize(bookList);

            File.WriteAllText(_pathFile, serializedList);
            return true;
        }

        public List<Book> GetAllBooks()
        {
            var content = File.ReadAllText(_pathFile);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Book>();
            }
            var bookList = JsonSerializer.Deserialize<List<Book>>(content);
            return bookList;
        }

        private long GetNewId(List<Book> list)
        {
            if(!list.Any())
            {
                return 1;
            }            
            return list.Max(x => x.Id) + 1;
        }

        public void UpdateBook(Book book)
        {
            var list = GetAllBooks();
            foreach (var item in list)
            {
                if (item.Id == book.Id)
                {
                    item.Author = book.Author;
                    item.Name = book.Name;
                    item.BuyPrice = book.BuyPrice;
                    item.SellPrice = book.SellPrice;
                }
            }
            string serializedList = JsonSerializer.Serialize(list);

            File.WriteAllText(_pathFile, serializedList);
        }

    }
}
