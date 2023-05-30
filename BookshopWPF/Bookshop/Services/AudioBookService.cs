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
        public List<AudioBook> GetAllAudioBooks(bool includeMissingAudioBooks)
        {
            using var db = new ProductDbContext();
            var audioBooks = db.AudioBooks.AsQueryable();
            if (!includeMissingAudioBooks)
            {
                audioBooks = audioBooks.Where(x => x.Quantity > 0);

            }

            return audioBooks.ToList();
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
            item.Author = audioBook.Author;
            item.Title = audioBook.Title;
            item.Genre = audioBook.Genre;
            item.BuyPrice = audioBook.BuyPrice;
            item.SellPrice = audioBook.SellPrice;
            item.Language = audioBook.Language;
            item.Description = audioBook.Description;
            item.Format = audioBook.Format;
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
