using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    class BookAvailableModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal SellPrice { get; set; } 
        
        public int Quantity { get; set; }


    }
}
