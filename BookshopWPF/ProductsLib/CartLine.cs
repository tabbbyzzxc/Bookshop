using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public class CartLine
    {
        public long Id { get; set; }

        public Cart Cart { get; set; }

        public long CartId { get; set; }

        public long BookId { get; set; }

        public int Quantity { get; set; }
    }
}
