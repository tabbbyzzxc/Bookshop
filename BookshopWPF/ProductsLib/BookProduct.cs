namespace Bookshop.ProductsLib
{
    public class BookProduct : Product
    {

        public BookProduct()
        {

        }

        public BookProduct(long id, string description, decimal sellPrice, decimal buyPrice, int quantity, string title, string author, string genre, string language) : base(id, description, sellPrice, buyPrice, quantity)
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

        public override string GetDescription()
        {
            return base.GetDescription() + $"Title: {Title}, Author: {Author}, Genre: {Genre}, Language: {Language} ";
        }

        public override string GetProductType()
        {
            throw new NotImplementedException();
        }
    }
}
