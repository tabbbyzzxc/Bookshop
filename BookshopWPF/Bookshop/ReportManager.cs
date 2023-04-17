using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    public class ReportManager
    {
        private List<Order> _orders;
        public ReportManager() 
        {
            
            
        }

        public Report MakeReport(DateTime from, DateTime to)
        { 
            var repo = new OrderRepository();
            var list = repo.SortOrdersViaDate(from, to);
            var report = new Report();
            
            foreach (var order in list)
            {
                var orderList = order.OrderList;
                foreach (var item in orderList)
                {
                    var reportLine = new ReportLine()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Author = item.Author,
                        SellPrice = item.SellPrice,
                        Quantity = item.Quantity,
                    };

                    foreach (var item2 in orderList)
                    {
                        if(reportLine.Id == item2.Id)
                        {
                            reportLine.Quantity += item2.Quantity;
                        }
                    }
                    report.ReportList.Add(reportLine);
                }

            }
            return report;
            
        }
    }
}
