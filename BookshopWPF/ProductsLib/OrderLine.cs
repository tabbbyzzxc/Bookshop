namespace Bookshop.ProductsLib
{
    public class OrderLine
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal SellPrice { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }

        public decimal Total
        {
            get
            {
                return SellPrice * Quantity;
            }
        }
    }
}
