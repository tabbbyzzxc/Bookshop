using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    public class Book
    {
        public Book()
        {
            
        }
        public Book(string name, string author, decimal gross, decimal net)
        {
            Name = name;
            Author = author;
            GrossPrice = gross;
            NetPrice = net;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal GrossPrice { get; set; }

        public decimal NetPrice { get; set; }



    }

    
}
