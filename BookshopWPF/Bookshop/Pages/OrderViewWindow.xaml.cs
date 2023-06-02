using Bookshop.ProductsLib;
using Bookshop.Services;
using System.Windows;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for OrderViewWindow.xaml
    /// </summary>
    public partial class OrderViewWindow : Window
    {
        private DocumentService _documentService = new DocumentService();
        private Order _order;

        public OrderViewWindow(Order order)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _order = order;
            cartDataGrid.ItemsSource = order.OrderList;
            orderNameLabel.Content = order.Name;
            orderDateLabel.Content = $"Date: {order.Date.ToString("dd.MM.yyyy")}";
            orderTotalLabel.Content = $"Total: {order.Total}";
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var check = _documentService.CreatePrintDocument(_order);
            new PrintWindow(check).ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
