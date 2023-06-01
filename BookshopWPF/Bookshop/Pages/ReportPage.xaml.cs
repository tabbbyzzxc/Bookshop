using System;
using System.Windows;
using System.Windows.Controls;
using Bookshop.Services;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
    
        public ReportPage()
        {
            InitializeComponent();
            fromDate.SelectedDate = DateTime.Now;
            dueDate.SelectedDate = DateTime.Now;
            //sortComboBox.SelectedIndex = 0;
        }

        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            if(fromDate.SelectedDate == null || dueDate.SelectedDate == null)
            {
                MessageBox.Show("Please select report dates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dateOption = sortComboBox.SelectedIndex;

           
            
            var reportManager = new ReportManager();
            var report = reportManager.MakeReport(dateOption, fromDate.SelectedDate.Value, dueDate.SelectedDate.Value);
            orderedListView.ItemsSource = report.OrderedProducts;
            returnedListView.ItemsSource = report.ReturnedProducts;
            totalOrderedAmount.Content = report.TotalOrderedAmount;
            totalOrderedQuantity.Content = report.TotalOrderedQuantity;
            totalReturnedAmount.Content = report.TotalReturnedAmount;
            totalReturnedQuantity.Content = report.TotalReturnedQuantity;
            totalAmount.Content = report.TotalAmount;
        }
    }

   
}
