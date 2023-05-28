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

            var due = dueDate.SelectedDate.Value;
            DateTime fromTime = fromDate.SelectedDate.Value;
            DateTime dueTime = new DateTime(due.Year, due.Month, due.Day, 23, 59, 59);
            
            var reportManager = new ReportManager();
            var report = reportManager.MakeReport(1,fromTime, dueTime);
            listView.ItemsSource = report.ReportList;
            totalAmount.Content = report.TotalAmount;
            totalQuantity.Content = report.TotalQuantity;
        }
    }

   
}
