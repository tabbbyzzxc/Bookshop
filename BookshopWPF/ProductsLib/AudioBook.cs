namespace Bookshop.ProductsLib
{
    public class AudioBook : BookProduct
    {
        public AudioBook()
        {

        }
        public AudioBook(long id, string description, decimal sellPrice, decimal buyPrice, int quantity, string title, string author, string genre, string language, string format)
            : base(id, description, sellPrice, buyPrice, quantity, title, author, genre, language)
        {
            Format = format;
        }

        public string Format { get; set; }


        public override string GetDescription()
        {
            return base.GetDescription();
        }

        public override string GetProductType()
        {
            return "Audio Book";
        }
    }
}
