namespace BookshopWeb.Models
{
    public class OrderBookListViewModel
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public decimal SellPrice { get; set; }

        public string ProductCode => $"BK000-{Id}";

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public string PaperType { get; set; }

        public int PageQuantity { get; set; }

        public long CartId { get; set; }
    }
}
