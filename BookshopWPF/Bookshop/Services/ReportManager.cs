using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class ReportManager
    {
        private InvoiceService _invoiceService = new InvoiceService();
        private OrderService _orderService = new OrderService();
        
        public Report MakeReport(int reportType, DateTime from, DateTime to)
        {
            to = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);
            List<Invoice> invoices;
            List<Order> filteredOrders;
            DateTime currentDate = DateTime.Now;
            
            switch (reportType)
            {
                case 1:
                    filteredOrders = _orderService.GetOrdersByDate(currentDate.Date, to);
                    invoices = _invoiceService.GetInvoicesByDate(currentDate.Date, to, InvoiceType.Return);
                    break;
                case 2:
                    filteredOrders = _orderService.GetOrdersByDate(currentDate.Date.AddDays(-(int)currentDate.Date.DayOfWeek + 1), to);
                    invoices = _invoiceService.GetInvoicesByDate(currentDate.Date.AddDays(-(int)currentDate.Date.DayOfWeek + 1), to, InvoiceType.Return);
                    break;
                case 3:
                    filteredOrders = _orderService.GetOrdersByDate(new DateTime(currentDate.Year, currentDate.Month, 1), to);
                    invoices = _invoiceService.GetInvoicesByDate(new DateTime(currentDate.Year, currentDate.Month, 1), to, InvoiceType.Return);
                    break;
                case 4:
                    filteredOrders = _orderService.GetOrdersByDate(new DateTime(currentDate.Year, 1 , 1, 0, 0, 0), to);
                    invoices = _invoiceService.GetInvoicesByDate(new DateTime(currentDate.Year, 1, 1, 0, 0, 0), to, InvoiceType.Return);
                    break;
                default:
                    filteredOrders = _orderService.GetOrdersByDate(from,to);
                    invoices = _invoiceService.GetInvoicesByDate(from, to, InvoiceType.Return);
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
