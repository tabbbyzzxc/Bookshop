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
