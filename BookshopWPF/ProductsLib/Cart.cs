using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.ProductsLib
{
    public class Cart
    {
        public Cart()
        {
            
        }

        public long Id { get; set; }

        //public long UserId { get; set; }

        //public User User { get; set; }

        public List<CartLine> CartLines { get; set; } = new List<CartLine>();

    }
}
