using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public class Report
    {
        public List<ReportLine> OrderedProducts { get; set; } = new List<ReportLine>();

        public List<ReportLine> ReturnedProducts { get; set; } = new List<ReportLine>();


        public decimal TotalOrderedAmount => OrderedProducts.Sum(x => x.Total);

        public int TotalOrderedQuantity => OrderedProducts.Sum(x =>x.Quantity);

        public decimal TotalReturnedAmount => ReturnedProducts.Sum(x => x.Total);

        public int TotalReturnedQuantity => ReturnedProducts.Sum(x => x.Quantity);

        public Decimal TotalAmount => TotalOrderedAmount - TotalReturnedAmount;

    }
}
