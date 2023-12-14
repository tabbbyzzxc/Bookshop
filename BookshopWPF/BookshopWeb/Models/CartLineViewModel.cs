using Bookshop.ProductsLib;

namespace BookshopWeb.Models
{
    public class CartLineViewModel
    {
        public long Id { get; set; }

        public string FrontId => $"CL{Id}";

        public Cart Cart { get; set; }

        public long CartId { get; set; }

        public Book Book { get; set; }

        public long BookId { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Quantity * Book.BuyPrice;
    }
}
