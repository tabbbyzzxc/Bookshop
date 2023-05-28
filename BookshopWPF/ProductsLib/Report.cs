using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public class Report
    {
        public List<ReportLine> ReportList { get; set; } = new List<ReportLine>();

        public decimal TotalAmount
        {
            get
            {
                return ReportList.Sum(x => x.Total);
            }
        }
        public int TotalQuantity
        {
            get
            {
                return ReportList.Sum(x => x.Quantity);
            }
        }
    }
}
