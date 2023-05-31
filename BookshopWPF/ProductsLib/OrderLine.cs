using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.ProductsLib
{
    public class OrderLine
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; } //change

        public int Quantity { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }

        [NotMapped]
        public Product Product { get; set; }

        public decimal Total => Quantity * Product.SellPrice;
        
    }
}
