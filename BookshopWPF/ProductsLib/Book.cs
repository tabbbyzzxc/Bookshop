namespace Bookshop.ProductsLib;

public class Book : BookProduct
{
    public Book()
    {

    }
    public Book(string description, decimal sellPrice, decimal buyPrice, string title, string author, string genre, string language, string paperType, int pageQuantity)
        : base(description, sellPrice, buyPrice, title, author, genre, language)
    {
        PaperType = paperType;
        PageQuantity = pageQuantity;
    }

    public string PaperType { get; set; }

    public int PageQuantity { get; set; }



    public override string MainData => $"{Author}. {Title}. {Genre}";

    public override string ProductType => "Book";

    public override string GetDescription()
    {
        return base.GetDescription() + $"Paper Type: {PaperType}, Page Quantity: {PageQuantity}";
    }

    public override Dictionary<string, string> GetProductInfoParameters()
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
