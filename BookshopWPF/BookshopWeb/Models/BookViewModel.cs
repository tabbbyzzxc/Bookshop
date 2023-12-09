namespace BookshopWeb.Models
{
    public class BookViewModel
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Description { get; set; }

        public decimal SellPrice { get; set; }

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
