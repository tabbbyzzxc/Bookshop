using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bookshop
{
    public class OrderRepository
    {
        private static string _path = "Database";
        private static string _pathFile = System.IO.Path.Combine(_path, "orderDB.txt");
        public OrderRepository() 
        {
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }
            if (!System.IO.File.Exists(_pathFile))
            {
                System.IO.File.Create(_pathFile);
            }
        }

        public bool PlaceOrder(Order order) 
        {
            if(order.Total==0)
            {
                return false;
            }
            var orderList = GetAllOrders();
            order.Id = GetNewId(orderList);

            orderList.Add(order);
            string serializedList = JsonSerializer.Serialize(orderList);

            File.WriteAllText(_pathFile, serializedList);
            return true;
        }

        public List<Order> GetAllOrders()
        {
            var content = File.ReadAllText(_pathFile);
            if (string.IsNullOrWhiteSpace(content))
            {
                return new List<Order>();
            }
            var orderList = JsonSerializer.Deserialize<List<Order>>(content);
            return orderList;
        }

        public List<Order> SortOrdersViaDate(DateTime fromDate, DateTime toDate)
        {   
            var sortedOrders = new List<Order>();

            var orders = GetAllOrders();
            foreach (var order in orders)
            {
                if (order.Date >= fromDate && order.Date <= toDate)
                {
                    sortedOrders.Add(order);

                }
            }
            return sortedOrders;
        }

        private long GetNewId(List<Order> list)
        {
            long maxId = 0;
            foreach (var item in list)
            {
                if (item.Id > maxId)
                {
                    maxId = item.Id;
                }
            }
            return maxId + 1;
        }

        
    }
}
