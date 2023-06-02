using Bookshop.ProductsLib;
using Bookshop.Services;
using Bookshop.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for ProductIncomePage.xaml
    /// </summary>
    public partial class ProductIncomePage : Page
    {
        private ObservableCollection<Product> _allProducts = new ObservableCollection<Product>();
        private ProductService _productService = new ProductService();
        private ObservableCollection<CartProductModel> _invoicedItems = new ObservableCollection<CartProductModel>();
        private InvoiceService _invoiceService = new InvoiceService();
        private DocumentService _documentService = new DocumentService();

        public ProductIncomePage()
        {
            InitializeComponent();
            _allProducts = new ObservableCollection<Product>(_productService.GetAllProducts());
            productsListView.ItemsSource = _allProducts;
            invoiceDataGrid.ItemsSource = _invoicedItems;
            invoiceDataGrid.CellEditEnding += InvoiceDataGrid_CellEditEnding;
        }

        private void InvoiceDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var inputText = ((TextBox)e.EditingElement).Text;
            var invoiced = invoiceDataGrid.SelectedItem as CartProductModel;
            if (invoiced != null && int.TryParse(inputText, out var quantity))
            {
                var product = _allProducts.First(x => x.UniqueId == invoiced.UniqueId);
                if (quantity > product.Quantity)
                {
                    ((TextBox)e.EditingElement).Text = product.Quantity.ToString();
                }
            }
        }

        private void InitDataGrid()
        {
            if (!invoiceDataGrid.Columns.Any())
            {
                return;
            }

            Style cellRightAlignmentStyle = new Style(typeof(DataGridCell));
            cellRightAlignmentStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));

            invoiceDataGrid.Columns[0].Header = "Code";
            invoiceDataGrid.Columns[0].Width = 80;
            invoiceDataGrid.Columns[0].IsReadOnly = true;

            invoiceDataGrid.Columns[1].Visibility = Visibility.Hidden;

            invoiceDataGrid.Columns[2].Width = 500;
            invoiceDataGrid.Columns[2].Header = "Product";
            invoiceDataGrid.Columns[2].IsReadOnly = true;

            invoiceDataGrid.Columns[3].Width = 80;
            invoiceDataGrid.Columns[3].Header = "Type";
            invoiceDataGrid.Columns[3].IsReadOnly = true;

            invoiceDataGrid.Columns[4].Width = 80; // price
            invoiceDataGrid.Columns[4].IsReadOnly = true;
            invoiceDataGrid.Columns[4].CellStyle = cellRightAlignmentStyle;

            invoiceDataGrid.Columns[5].Width = 100; // quantity
            invoiceDataGrid.Columns[5].CellStyle = cellRightAlignmentStyle;

            invoiceDataGrid.Columns[6].Width = 100; // total
            invoiceDataGrid.Columns[6].IsReadOnly = true;
            invoiceDataGrid.Columns[6].CellStyle = cellRightAlignmentStyle;
        }

        private void OnProductItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = productsListView.SelectedItem as Product;
            if (selectedItem != null)
            {
                new ProductAddQuantityWindow(selectedItem, _invoicedItems, InvoiceType.Income).ShowDialog();
                invoiceDataGrid.Items.Refresh();
            }
        }

        private void OpenInvoice_Click(object sender, RoutedEventArgs e)
        {
            TogglePages();
            InitDataGrid();
        }

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            if (!_invoicedItems.Any())
            {
                return;
            }

            Invoice invoice = new Invoice()
            {
                Date = DateTime.Now,
                InvoiceType = InvoiceType.Income,
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
            productsListView.ItemsSource = _allProducts;
            TogglePages();
            
            var check = _documentService.CreatePrintDocument(invoice);
            new PrintWindow(check).ShowDialog();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            TogglePages();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            _invoicedItems.Clear();
        }

        private void TogglePages()
        {
            NewOrderPage.Visibility = NewOrderPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            CartPage.Visibility = CartPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
