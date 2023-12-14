using Bookshop.ProductsLib;

namespace BookshopWeb.Models
{
    public class CartListViewModel
    {
        public List<CartLineViewModel> CartLines { get; set; }

        public decimal Total => CartLines.Sum(x => x.Total);
    }
}
