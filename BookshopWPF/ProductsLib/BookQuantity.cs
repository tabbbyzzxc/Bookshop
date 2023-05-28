namespace Bookshop.ProductsLib
{
    public class BookQuantity
    {

        public long BookId { get; set; }

        public int Quantity { get; set; }

        public BookQuantity()
        {

        }
        public BookQuantity(long id, int quant)
        {
            BookId = id;
            Quantity = quant;
        }
    }
}
