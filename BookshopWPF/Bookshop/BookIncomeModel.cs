using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    internal class BookIncomeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal BuyPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get
            {
                return BuyPrice * Quantity;
            } }
    }
}
