namespace Bookshop.ProductsLib;

public class Book : BookProduct
{
    public Book()
    {

    }
    public Book(long id, string description, decimal sellPrice, decimal buyPrice, int quantity, string title, string author,string genre, string language, string paperType, int pageQuantity)
        : base(id, description, sellPrice, buyPrice, quantity, title, author, genre, language)
    {
        PaperType = paperType;
        PageQuantity = pageQuantity;
    }

    public string PaperType { get; set; }

    public int PageQuantity { get; set; }


    public override string GetDescription()
    {
        return base.GetDescription();
    }

    public override string GetProductType()
    {
        return "Book";
    }
}
