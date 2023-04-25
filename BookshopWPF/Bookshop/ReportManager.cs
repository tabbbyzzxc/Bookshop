﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookshop.Models;
using Bookshop.Repositories;

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