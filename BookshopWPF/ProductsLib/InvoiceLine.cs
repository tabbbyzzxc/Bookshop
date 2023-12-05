using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.ProductsLib
{
    public class InvoiceLine
    {
        public long Id { get; set; }

        public Guid ProductUniqueId { get; set; }

        public int Quantity { get; set; }

        public Invoice Invoice { get; set; }

        public long InvoiceId { get; set; }

        [NotMapped]
        public Book Product { get; set; }

        public decimal Total => Quantity * Product.SellPrice;
    }
}