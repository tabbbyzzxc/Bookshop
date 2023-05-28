using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class OrderService
    {
        public bool AddOrder(Order order)
        {
            if (order.Total == 0)
            {
                return false;
            }

            var db = new ProductDbContext();
            db.Orders.Add(order);
            db.SaveChanges();
            return true;
        }

        public List<Order> GetAllOrders()
        {
            var db = new ProductDbContext();
            return db.Orders.Include(x => x.OrderList).ToList();
        }

        public List<Order> GetOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            var db = new ProductDbContext();
            var filteredOrders = db.Orders.Where(x => x.Date >= fromDate && x.Date <= toDate).ToList();
            return filteredOrders;
        }

    }
}
