using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public class ReportLine
    {
        public Guid ProductUniqueId { get; set; }

        public decimal SellPrice { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public decimal Total => SellPrice * Quantity;
        
    }
}
