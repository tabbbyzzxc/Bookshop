﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ProductsLib
{
    public class Order
    {
        public long Id { get; set; }

        public string Name => $"Order №000{Id}";

        public DateTime Date { get; set; }

        public List<OrderLine> OrderList { get; set; } = new List<OrderLine>();

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (var book in OrderList)
                {
                    total += book.Total;
                }
                return total;
            }
        }

    }
}
