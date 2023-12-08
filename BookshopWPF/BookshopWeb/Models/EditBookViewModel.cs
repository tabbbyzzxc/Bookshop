namespace BookshopWeb.Models
{
    public class EditBookViewModel
    {
        public long Id { get; }

        public string Description { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public string PaperType { get; set; }

        public int PageQuantity { get; set; }

    }
}
