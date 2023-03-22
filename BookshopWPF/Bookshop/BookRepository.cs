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
        private static string _path = @"C:\Users\User\source\repos\BookshopWPF\Database";
        private static string _pathFile = System.IO.Path.Combine(_path, "dbFile.txt");

      
        public void AddBook(Book book)
        {
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }
            if(!System.IO.File.Exists(_pathFile))
            {
                System.IO.File.Create(_pathFile);
            }
            var bookList = GetAllBooks();


            bookList.Add(book);
            string serializedList = JsonSerializer.Serialize(bookList);

            File.WriteAllText(_pathFile, serializedList);
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

        public bool DuplicateCheck(Book book)
        {
            var list = GetAllBooks();
            foreach (var item in list)
            {
                if(item.Author == book.Author && item.Name== book.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
