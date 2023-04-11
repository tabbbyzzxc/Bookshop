using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookshop
{


    internal class BookRepository
    {
        private static string _path = "Database";
        private static string _pathFile = System.IO.Path.Combine(_path, "dbFile.txt");

        public BookRepository()
        {
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }
            if (!System.IO.File.Exists(_pathFile))
            {
                System.IO.File.Create(_pathFile);
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
            long maxId = 0;
            foreach (var item in list)
            {
                if (item.Id > maxId)
                {
                    maxId = item.Id;
                }
            }
            return maxId + 1;
        }

        public void UpdateBook(Book book)
        {
            var list = GetAllBooks();
            foreach(var item in list)
            {
                if(item.Id == book.Id)
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
