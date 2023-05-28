using Bookshop.ProductsLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class ReportManager
    {
        public Report MakeReport(int reportType, DateTime from, DateTime to)
        {
            var repo = new OrderService();
            DateTime currentDate = DateTime.Now;
            List<Order> filteredOrders = new List<Order>();
            switch (reportType)
            {
                case 1:
                    filteredOrders = repo.GetOrdersByDate(new DateTime(currentDate.Day), to);
                    break;
                case 2:
                    filteredOrders = repo.GetOrdersByDate(new DateTime((currentDate.DayOfYear + 6) / 7), to);
                    break;
                case 3:
                    filteredOrders = repo.GetOrdersByDate(new DateTime(currentDate.Year, currentDate.Month, 1), to);
                    break;
                case 4:
                    filteredOrders = repo.GetOrdersByDate(new DateTime(currentDate.Year), to);
                    break;
                default:
                    filteredOrders = repo.GetOrdersByDate(from, to);
                    break;
            }
            var report = new Report();


            foreach (var order in filteredOrders)
            {
                var orderList = order.OrderList;
                foreach (var item in orderList)
                {
                    var reportLine = report.ReportList.FirstOrDefault(x => x.Id == item.Id);
                    if (reportLine == null)
                    {
                        report.ReportList.Add(new ReportLine()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Author = item.Author,
                            SellPrice = item.SellPrice,
                            Quantity = item.Quantity,
                        });
                    }
                    else
                    {
                        reportLine.Quantity += item.Quantity;
                    }
                }
            }
            return report;
        }


    }
}
