using System;

namespace Bookshop.ProductsLib
{
    public class ReportLine
    {
        public Guid ProductUniqueId { get; set; }

        public int Quantity { get; set; }

        public Book Product { get; set; }

        public decimal Total
        {
            get
            {
                if(Product == null)
                {
                    return 0;
                }
                return Product.SellPrice * Quantity;
            }
        }
    }
}
