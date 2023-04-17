using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookshop
{
    class BookQuantity
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
