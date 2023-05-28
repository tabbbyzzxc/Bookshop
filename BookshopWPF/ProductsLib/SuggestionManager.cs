using Bookshop.ProductsLib.Repositories;

namespace Bookshop.ProductsLib
{
    public class SuggestionManager
    {
            
        private BookRepository _repo = new BookRepository();

        public List<Book> GetRecommendedBooks(string genre)
        {

            Random rnd = new Random();
            var bookList = _repo.GetAllBooks().Where(x => x.Genre == genre).ToList();
            var recommendedBooksList = new List<Book>();
            for (int i = 0; i < 3; i++)
            {
                var number = rnd.Next(0, bookList.Count);
                recommendedBooksList.Add(bookList[i]);
            }
            return recommendedBooksList;
        }

        public List<Book> GetRecommendedAudioBooks(string genre)
        {

            Random rnd = new Random();
            var bookList = _repo.GetAllBooks().Where(x => x.Genre == genre).ToList();
            var recommendedBooksList = new List<Book>();
            for (int i = 0; i < 3; i++)
            {
                var number = rnd.Next(0, bookList.Count);
                recommendedBooksList.Add(bookList[i]);
            }
            return recommendedBooksList;
        }
    }
}
