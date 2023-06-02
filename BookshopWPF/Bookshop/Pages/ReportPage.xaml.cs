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
        private ReportManager _reportManager = new ReportManager();

        public ReportPage(ReportType reportType)
        {
            InitializeComponent();
            fromDate.SelectedDate = DateTime.Now;
            dueDate.SelectedDate = DateTime.Now;
            sortComboBox.SelectedIndex = (int)reportType;
            if (reportType != ReportType.Custom)
            {
                RenderReport(reportType);
            }
        }

        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            if (fromDate.SelectedDate == null || dueDate.SelectedDate == null)
            {
                MessageBox.Show("Please select report dates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var choice = sortComboBox.SelectedIndex;
            RenderReport((ReportType)choice);
        }

        private void RenderReport(ReportType reportType)
        {
            var report = _reportManager.MakeReport(reportType, fromDate.SelectedDate.Value, dueDate.SelectedDate.Value);
            orderedListView.ItemsSource = report.OrderedProducts;
            returnedListView.ItemsSource = report.ReturnedProducts;
            totalOrderedAmount.Content = report.TotalOrderedAmount;
            totalOrderedQuantity.Content = report.TotalOrderedQuantity;
            totalReturnedAmount.Content = report.TotalReturnedAmount;
            totalReturnedQuantity.Content = report.TotalReturnedQuantity;
            totalAmount.Content = report.TotalAmount;
        }

        private void SetControlVisibility(ReportType reportType)
        {
            if (calendarPanel != null)
            {
                calendarPanel.Visibility = reportType == ReportType.Custom ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var reportType = (ReportType)sortComboBox.SelectedIndex;
            SetControlVisibility(reportType);
        }
    }
}
