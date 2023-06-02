using Bookshop.ProductsLib;
using Bookshop.Services;
using System.Windows;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for InvoiceViewWindow.xaml
    /// </summary>
    public partial class InvoiceViewWindow : Window
    {
        private DocumentService _documentService = new DocumentService();
        private Invoice _invoice;

        public InvoiceViewWindow(Invoice invoice)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _invoice = invoice;
            cartDataGrid.ItemsSource = invoice.InvoiceLines;
            orderNameLabel.Content = invoice.Name;
            orderDateLabel.Content = $"Date: {invoice.Date.ToString("dd.MM.yyyy")}";
            orderTotalLabel.Content = $"Total: {invoice.Total}";
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var check = _documentService.CreatePrintDocument(_invoice);
            new PrintWindow(check).ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
