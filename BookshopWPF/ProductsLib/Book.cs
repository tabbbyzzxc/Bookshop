using System;
using System.Collections.Generic;

namespace Bookshop.ProductsLib
{
    public class Book
    {
        public Book()
        {
            
        }

        public Book(string desc, decimal tempSell, decimal tempBuy, string tempTitle, string tempAuth, string tempGenre, string tempLang, string tempPaperType, int tempPageCount)
        {
            Description = desc;
            SellPrice = tempSell;
            BuyPrice = tempBuy;
            Title = tempTitle;
            Author = tempAuth;
            Genre = tempGenre;
            Language = tempLang;
            PaperType = tempPaperType;
            PageQuantity = tempPageCount;
        }

        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Description { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public int Quantity { get; set; }

        public string MainData => $"{Author}. {Title}. {Genre}";

        public string ProductType => "Book";

        public string ProductCode => $"BK000-{Id}";

        public string Сharacteristics => $"Author: {Author}. Title: {Title}. Genre: {Genre}. Language: {Language}. Paper type: {PaperType}. Page quantity: {PageQuantity}";

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public string PaperType { get; set; }

        public int PageQuantity { get; set; }

        public Dictionary<string, string> GetProductInfoParameters()
        {
            return new Dictionary<string, string>
            {
                { "Author", Author },
                { "Title", Title },
                { "Genre", Genre },
                { "Language", Language },
                { "Paper type", PaperType },
                { "Page count", PageQuantity.ToString()},
                { "Description", Description},
                { "Price", SellPrice.ToString()}
             };
        }

        

    }
}
