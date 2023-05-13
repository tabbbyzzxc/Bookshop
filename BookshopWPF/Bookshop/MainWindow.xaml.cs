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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Page page;
            switch (button.Name)
            {
                case "CheckoutButton":
                    page = new CheckoutPage();
                    break;
                case "ReportButton":
                    page = new ReportPage();
                    break;
                case "AddButton":
                    page = new AddBookPage();
                    break;
                case "EditButton":
                    page = new EditPage();
                    break;
                case "ReceiptButton":
                    page = new ReceiptPage();
                    break;
                case "MissingButton":
                    page = new MissingPage();
                    break;
                default:
                    page = new WelcomePage();
                    break;
            }
            Frame1.Content = page;
        }
    }
}
