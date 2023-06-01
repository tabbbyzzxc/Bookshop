using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{

    public enum InvoiceType
    {
        Income,
        Return
    }

    public class Invoice
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

        public InvoiceType InvoiceType { get; set; }

        public decimal Total => InvoiceLines.Sum(x => x.Total);
    }
}
