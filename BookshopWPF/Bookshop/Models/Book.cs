namespace Bookshop.Models
{
    public class Book
    {
        public Book()
        {

        }
        public Book(string name, string author, decimal buy, decimal sell)
        {
            Name = name;
            Author = author;
            BuyPrice = buy;
            SellPrice = sell;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }



    }


}
