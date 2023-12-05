using Bookshop.Pages;
using Bookshop.Services;
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

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Frame1.Content = new WelcomePage();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var button = (MenuItem)sender;
            Page page;
            switch (button.Header)
            {
                case "New order":
                    page = new OrderPage();
                    break;
                case "Book":
                    page = new AddBookPage();
                    break;
                case "Edit product":
                    page = new EditPage();
                    break;
                case "Income Invoice":
                    page = new ProductIncomePage();
                    break;
                case "Return Invoice":
                    page = new ReturnInvoicePage();
                    break;
                case "Day":
                    page = new ReportPage(ReportType.Day);
                    break;
                case "Week":
                    page = new ReportPage(ReportType.Week);
                    break;
                case "Month":
                    page = new ReportPage(ReportType.Month);
                    break;
                case "Year":
                    page = new ReportPage(ReportType.Year);
                    break;
                case "Custom":
                    page = new ReportPage(ReportType.Custom);
                    break;
                case "Missing Products":
                    page = new MissingProductsPage();
                    break;
                case "View orders":
                    page = new OrderListPage();
                    break;
                case "View invoices":
                    page = new InvoiceListPage();
                    break;
                default:
                    page = new WelcomePage();
                    break;
            }
            Frame1.Content = page;
        }
    }
}
