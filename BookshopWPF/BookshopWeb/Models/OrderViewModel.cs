using Bookshop.ProductsLib;

namespace BookshopWeb.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }

        public string AccordeonId => $"book{Id}";

        public string Name => $"Order №000{Id}";

        public DateTime Date { get; set; }

        public List<OrderLine> OrderList { get; set; } = new List<OrderLine>();

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (var book in OrderList)
                {
                    total += book.Total;
                }
                return total;
            }
        }
    }
}
