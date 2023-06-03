namespace Bookshop.ProductsLib
{
    public abstract class BookProduct : Product
    {

        public BookProduct()
        {

        }

        public BookProduct(string description, decimal sellPrice, decimal buyPrice, string title, string author, string genre, string language) : base(description, sellPrice, buyPrice)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Language = language;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public override string Сharacteristics => $"Author: {Author}. Title: {Title}. Genre: {Genre}. Language: {Language}.";
    }
}
