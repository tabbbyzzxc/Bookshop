using Bookshop.ProductsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Services
{
    public class AudioBookService
    {
        public List<AudioBook> GetAllAudioBooks()
        {
            using var db = new ProductDbContext();
            return db.AudioBooks.Where(x => x.Quantity > 0).ToList();
        }

        public bool AddAudioBook(AudioBook audioBook)
        {
            using var db = new ProductDbContext();
            var exist = db.AudioBooks.FirstOrDefault(x => x.Title == audioBook.Title && x.Author == audioBook.Author && x.Format == audioBook.Format);
            if (exist != null)
            {
                return false;
            }

            db.AudioBooks.Add(audioBook);
            db.SaveChanges();

            return true;
        }

        public void UpdateAudioBook(AudioBook audioBook)
        {
            using var db = new ProductDbContext();
            var item = db.AudioBooks.FirstOrDefault(x => x.Id == audioBook.Id);
            if (item == null)
            {
                return;
            }

            db.Update(item);
            db.SaveChanges();

        }

        public List<AudioBook> GetMissingAudioBooks()
        {
            using var db = new ProductDbContext();
            return db.AudioBooks.Where(x => x.Quantity == 0).ToList();
        }
    }
}
