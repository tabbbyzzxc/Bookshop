using Bookshop.ProductsLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public enum ReportType
    {
        Day = 1,
        Week,
        Month,
        Year,
        Custom
    }

    public class ReportManager
    {
        private InvoiceService _invoiceService = new InvoiceService();
        private OrderService _orderService = new OrderService();
        
        public Report MakeReport(ReportType reportType, DateTime from, DateTime to)
        {   
            List<Invoice> invoices;
            List<Order> filteredOrders;
            DateTime currentDate = DateTime.Now;
            var endOfTheDay = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 23, 59, 59);
            switch (reportType)
            {
                case ReportType.Day:
                    filteredOrders = _orderService.GetOrdersByDate(currentDate.Date, endOfTheDay);
                    invoices = _invoiceService.GetInvoicesByDate(currentDate.Date, endOfTheDay, InvoiceType.Return);
                    break;
                case ReportType.Week:
                    filteredOrders = _orderService.GetOrdersByDate(currentDate.Date.AddDays(-(int)currentDate.Date.DayOfWeek + 1), endOfTheDay);
                    invoices = _invoiceService.GetInvoicesByDate(currentDate.Date.AddDays(-(int)currentDate.Date.DayOfWeek + 1), endOfTheDay, InvoiceType.Return);
                    break;
                case ReportType.Month:
                    filteredOrders = _orderService.GetOrdersByDate(new DateTime(currentDate.Year, currentDate.Month, 1), endOfTheDay);
                    invoices = _invoiceService.GetInvoicesByDate(new DateTime(currentDate.Year, currentDate.Month, 1), endOfTheDay, InvoiceType.Return);
                    break;
                case ReportType.Year:
                    filteredOrders = _orderService.GetOrdersByDate(new DateTime(currentDate.Year, 1 , 1, 0, 0, 0), endOfTheDay);
                    invoices = _invoiceService.GetInvoicesByDate(new DateTime(currentDate.Year, 1, 1, 0, 0, 0), endOfTheDay, InvoiceType.Return);
                    break;
                default:
                    filteredOrders = _orderService.GetOrdersByDate(from, new DateTime(to.Year, to.Month, to.Day, 23, 59, 59));
                    invoices = _invoiceService.GetInvoicesByDate(from, new DateTime(to.Year, to.Month, to.Day, 23, 59, 59), InvoiceType.Return);
                    break;
            }
            var report = new Report();


            foreach (var order in filteredOrders)
            {
                var orderList = order.OrderList;
                foreach (var item in orderList)
                {
                    var reportLine = report.OrderedProducts.FirstOrDefault(x => x.ProductUniqueId == item.ProductUniqueId);
                    if (reportLine == null)
                    {
                        report.OrderedProducts.Add(new ReportLine()
                        {
                            ProductUniqueId = item.ProductUniqueId,
                            Quantity = item.Quantity,
                            Product = item.Book
                        });
                    }
                    else
                    {
                        reportLine.Quantity += item.Quantity;
                    }
                }
            }

            foreach (var invoice in invoices)
            {
                var invoiceList = invoice.InvoiceLines;
                foreach (var item in invoiceList)
                {
                    var reportLine = report.ReturnedProducts.FirstOrDefault(x => x.ProductUniqueId == item.ProductUniqueId);
                    if (reportLine == null)
                    {
                        report.ReturnedProducts.Add(new ReportLine()
                        {
                            ProductUniqueId = item.ProductUniqueId,
                            Quantity = item.Quantity,
                            Product = item.Product
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
