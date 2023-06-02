using Bookshop.ProductsLib;
using Bookshop.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        private OrderService _orderService = new OrderService();

        public OrderListPage()
        {
            InitializeComponent();
            fromDate.SelectedDate = DateTime.Now;
            dueDate.SelectedDate = DateTime.Now;
        }

        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            if (fromDate.SelectedDate == null || dueDate.SelectedDate == null)
            {
                MessageBox.Show("Please select order dates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var from = fromDate.SelectedDate.Value;
            var to = dueDate.SelectedDate.Value;
            var orders = _orderService.GetOrdersByDate(from, new DateTime(to.Year, to.Month, to.Day, 23, 59, 59));
            orderedListView.ItemsSource = orders;
        }

        private void orderedListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = orderedListView.SelectedItem as Order;
            if (selectedItem != null)
            {
                new OrderViewWindow(selectedItem).ShowDialog();
            }
        }
    }
}
