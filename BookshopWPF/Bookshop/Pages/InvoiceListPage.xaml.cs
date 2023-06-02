using Bookshop.ProductsLib;
using Bookshop.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for InvoiceListPage.xaml
    /// </summary>
    public partial class InvoiceListPage : Page
    {
        private InvoiceService _invoiceService = new InvoiceService();

        public InvoiceListPage()
        {
            InitializeComponent();
            fromDate.SelectedDate = DateTime.Now;
            dueDate.SelectedDate = DateTime.Now;
        }

        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            if (fromDate.SelectedDate == null || dueDate.SelectedDate == null)
            {
                MessageBox.Show("Please select invoice dates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var option = sortComboBox.SelectedIndex;
            var from = fromDate.SelectedDate.Value;
            var to = dueDate.SelectedDate.Value;
            var orders = _invoiceService.GetInvoicesByDate(from, new DateTime(to.Year, to.Month, to.Day, 23, 59, 59), (InvoiceType)option);
            invoiceListView.ItemsSource = orders;
        }

        private void orderedListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = invoiceListView.SelectedItem as Invoice;
            if (selectedItem != null)
            {
                new InvoiceViewWindow(selectedItem).ShowDialog();
            }
        }
    }
}
