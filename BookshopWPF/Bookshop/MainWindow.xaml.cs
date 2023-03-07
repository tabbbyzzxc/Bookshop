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

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new CheckoutPage();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new ReportPage();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new AddBookPage();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new EditPage();
        }

        private void ReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new ReceiptPage();
        }

        private void MissingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame1.Content = new MissingPage();
        }
    }
}
