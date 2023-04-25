using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using Bookshop.Models;
using Bookshop.Repositories;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private ObservableCollection<Order> _orders;
        public ReportPage()
        {
            var repo = new OrderRepository();
            _orders = new ObservableCollection<Order>(repo.GetAllOrders());
            InitializeComponent();
            listView.ItemsSource = _orders;
            
            
        }

        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            DateTime fromTime = Convert.ToDateTime(from.Text);
            DateTime toTime = Convert.ToDateTime(to.Text);
            var report = new ReportManager();
            report.MakeReport(fromTime, toTime);
        }
    }

   
}
