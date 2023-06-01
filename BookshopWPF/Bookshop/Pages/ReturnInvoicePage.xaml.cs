using Bookshop.ProductsLib;
using Bookshop.Services;
using Bookshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ReturnInvoicePage.xaml
    /// </summary>
    public partial class ReturnInvoicePage : Page
    {
        private ObservableCollection<Product> _allProducts = new ObservableCollection<Product>();
        private ProductService _productService = new ProductService();
        private ObservableCollection<CartProductModel> _invoicedItems = new ObservableCollection<CartProductModel>();
        private InvoiceService _invoiceService = new InvoiceService();
        public ReturnInvoicePage()
        {
            InitializeComponent();
            _allProducts = new ObservableCollection<Product>(_productService.GetAllProducts());
            listView.ItemsSource = _allProducts;
            orderDataGrid.ItemsSource = _invoicedItems;
            orderDataGrid.CellEditEnding += OrderDataGrid_CellEditEnding;
        }

        private void OrderDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var inputText = ((TextBox)e.EditingElement).Text;
            var ordered = orderDataGrid.SelectedItem as CartProductModel;
            if (ordered != null && int.TryParse(inputText, out var quantity))
            {
                var product = _allProducts.First(x => x.UniqueId == ordered.UniqueId);
                if (quantity > product.Quantity)
                {
                    ordered.Quantity = product.Quantity + 1;
                    ((TextBox)e.EditingElement).Text = (product.Quantity + 1).ToString();
                    product.Quantity -= ordered.Quantity;
                }
                else
                {
                    product.Quantity -= quantity - 1;
                }

            }

        }

        private void InitDataGrid()
        {
            if (!orderDataGrid.Columns.Any())
            {
                return;
            }

            orderDataGrid.Columns[0].Visibility = Visibility.Hidden;
            orderDataGrid.Columns[1].Visibility = Visibility.Hidden;
            orderDataGrid.Columns[2].Width = 300;
            orderDataGrid.Columns[2].Header = "Product";
            orderDataGrid.Columns[2].IsReadOnly = true;
            orderDataGrid.Columns[3].Width = 100;
            orderDataGrid.Columns[3].Header = "Type";
            orderDataGrid.Columns[3].IsReadOnly = true;
            orderDataGrid.Columns[4].Width = 100;
            orderDataGrid.Columns[4].IsReadOnly = true;

            orderDataGrid.Columns[5].Width = 100; // quantity

            orderDataGrid.Columns[6].Width = 100;
            orderDataGrid.Columns[6].IsReadOnly = true;
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = listView.SelectedItem as Product;
            if (selectedItem != null)
            {
                new ProductAddQuantityWindow(selectedItem, _invoicedItems).ShowDialog();

                orderDataGrid.Items.Refresh();
                listView.Items.Refresh();
            }
        }

        private void OpenInvoice_Click(object sender, RoutedEventArgs e)
        {
            TogglePages();

            InitDataGrid();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            Invoice invoice = new Invoice()
            {
                Date = DateTime.Now,
                InvoiceType = InvoiceType.Return,
                InvoiceLines = _invoicedItems.Select(x => new InvoiceLine()
                {
                    ProductUniqueId = x.UniqueId,
                    Quantity = x.Quantity,
                    Product = _allProducts.First(y => y.UniqueId == x.UniqueId)

                }).ToList()
            };
            _invoiceService.AddInvoice(invoice);
            _invoicedItems.Clear();
            _allProducts = new ObservableCollection<Product>(_productService.GetAllProducts());
            listView.ItemsSource = _allProducts;
            TogglePages();

        }


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {

            TogglePages();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _invoicedItems)
            {
                var product = _allProducts.First(x => x.UniqueId == item.UniqueId);
                product.Quantity += item.Quantity;
            }

            listView.Items.Refresh();
            _invoicedItems.Clear();
        }

        private void TogglePages()
        {
            NewOrderPage.Visibility = NewOrderPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            CartPage.Visibility = CartPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
