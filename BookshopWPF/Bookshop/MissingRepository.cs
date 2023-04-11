using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    internal class MissingRepository
    {

        private List<Book> _bookRepo;
        private List<BookQuantity> _idRepo;

        public List<Book> Find() 
        {
            BookRepository repo = new BookRepository();
            BookQuantityRepository idRepo = new BookQuantityRepository();
            _bookRepo = repo.GetAllBooks();
            _idRepo = idRepo.GetBooksIds();
            var missingBooksIds = new List<BookQuantity>();
            var missingBooks = new List<Book>();
            foreach (var book in _idRepo) 
            {
                if(book.Quantity == 0)
                {
                    missingBooksIds.Add(book);
                }
            }
            for (int i = 0; i < _bookRepo.Count; i++)
            {
                for (int j = 0; j < missingBooksIds.Count; j++)
                {
                    if (_bookRepo[i].Id == missingBooksIds[j].BookId)
                    {
                        missingBooks.Add(_bookRepo[i]);
                    }
                }
            }
            return missingBooks;

        }
    }
}
