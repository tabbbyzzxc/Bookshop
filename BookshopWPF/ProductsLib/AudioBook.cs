namespace Bookshop.ProductsLib
{
    public class AudioBook : BookProduct
    {
        public AudioBook()
        {

        }
        public AudioBook(string description, decimal sellPrice, decimal buyPrice, string title, string author, string genre, string language, string format)
            : base(description, sellPrice, buyPrice, title, author, genre, language)
        {
            Format = format;
        }

        public string Format { get; set; }

        public override string MainData => $"{Author}. {Title}. {Genre}";

        public override string ProductType => "Audio Book";

        public override string ProductCode => $"ABK000-{Id}";

        public override string Сharacteristics => $"{base.Сharacteristics} Format: {Format}";

        public override Dictionary<string, string> GetProductInfoParameters()
        {
            return new Dictionary<string, string>
        {
            { "Author", Author },
            { "Title", Title },
            { "Genre", Genre },
            { "Language", Language },
            { "Format", Format },
            { "Description", Description},
            { "Price", SellPrice.ToString()}
        };
        }

    }
}
