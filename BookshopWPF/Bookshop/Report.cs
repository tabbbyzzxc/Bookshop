using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    public class Report
    {
        public List<ReportLine> ReportList { get; set; } = new List<ReportLine>();

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (var book in ReportList)
                {
                    total += book.Total;
                }
                return total;
            }
        }
    }
}
