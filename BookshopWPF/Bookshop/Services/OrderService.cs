using Bookshop.ProductsLib;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class OrderService
    {
        private ProductService productService = new ProductService();

        public bool AddOrder(Order order)
        {
            var db = new ProductDbContext();
            db.Orders.Add(order);
            db.SaveChanges();
            productService.UpdateQuantities(order);

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
            var filteredOrders = db.Orders
                .Where(x => x.Date >= fromDate && x.Date <= toDate)
                .Include(x => x.OrderList)
                .ToList();
            var productIds = filteredOrders.SelectMany(x => x.OrderList).Distinct().Select(x => x.ProductUniqueId).ToList();
            var products = productService.GetProductsByIds(productIds);
            foreach (var order in filteredOrders)
            {
                foreach (var orderline in order.OrderList)
                {
                    orderline.Product = products.FirstOrDefault(x => x.UniqueId == orderline.ProductUniqueId);
                }
            }

            return filteredOrders;
        }
    }
}
