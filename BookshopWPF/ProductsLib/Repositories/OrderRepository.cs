using System.Text.Json;
using Bookshop.ProductsLib;

namespace Bookshop.ProductsLib.Repositories
{
    public class OrderRepository
    {
        private static string _path = "Database";
        private static string _pathFile = Path.Combine(_path, "orderDB.txt");
        public OrderRepository()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            if (!File.Exists(_pathFile))
            {
                File.Create(_pathFile);
            }
        }

        public bool SaveOrder(Order order)
        {
            if (order.Total == 0)
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

        public List<Order> GetOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            var orders = GetAllOrders();
            var filteredOrders = orders.Where(x => x.Date >= fromDate && x.Date <= toDate).ToList();
            return filteredOrders;
        }

        private long GetNewId(List<Order> list)
        {

            if (!list.Any())
            {
                return 1;
            }
            return list.Max(x => x.Id) + 1;
        }


    }
}
