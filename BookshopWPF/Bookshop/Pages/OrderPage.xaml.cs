using Bookshop.ProductsLib;
using Bookshop.Services;
using Bookshop.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private ObservableCollection<Book> _allProducts;
        private ProductService _productService = new ProductService();
        private ObservableCollection<CartProductModel> _orderedProducts = new ObservableCollection<CartProductModel>();
        private OrderService _orderService = new OrderService();
        private SuggestionManager _suggestionManager = new SuggestionManager();
        private DocumentService _documentService = new DocumentService();

        public OrderPage()
        {
            InitializeComponent();
            _allProducts = new ObservableCollection<Book>(_productService.GetAvailableProducts());
            productsListView.ItemsSource = _allProducts;
            cartDataGrid.ItemsSource = _orderedProducts;
            cartDataGrid.CellEditEnding += CartDataGrid_CellEditEnding;
        }

        private void CartDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var inputText = ((TextBox)e.EditingElement).Text;
            var orderedProduct = cartDataGrid.SelectedItem as CartProductModel;
            if (orderedProduct != null && int.TryParse(inputText, out var quantity))
            {
                var product = _allProducts.First(x => x.UniqueId == orderedProduct.UniqueId);
                if (quantity > product.Quantity)
                {
                    ((TextBox)e.EditingElement).Text = product.Quantity.ToString();
                }
            }
        }

        private void InitDataGrid()
        {
            if (!cartDataGrid.Columns.Any())
            {
                return;
            }
            
            Style cellRightAlignmentStyle = new Style(typeof(DataGridCell));
            cellRightAlignmentStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Right));

            cartDataGrid.Columns[0].Header = "Code";
            cartDataGrid.Columns[0].Width = 80;
            cartDataGrid.Columns[0].IsReadOnly = true;

            cartDataGrid.Columns[1].Visibility = Visibility.Hidden;

            cartDataGrid.Columns[2].Width = 500;
            cartDataGrid.Columns[2].Header = "Product";
            cartDataGrid.Columns[2].IsReadOnly = true;

            cartDataGrid.Columns[3].Width = 80;
            cartDataGrid.Columns[3].Header = "Type";
            cartDataGrid.Columns[3].IsReadOnly = true;

            cartDataGrid.Columns[4].Width = 80; // price
            cartDataGrid.Columns[4].IsReadOnly = true;
            cartDataGrid.Columns[4].CellStyle = cellRightAlignmentStyle;

            cartDataGrid.Columns[5].Width = 100; // quantity
            cartDataGrid.Columns[5].CellStyle = cellRightAlignmentStyle;

            cartDataGrid.Columns[6].Width = 100; // total
            cartDataGrid.Columns[6].IsReadOnly = true;
            cartDataGrid.Columns[6].CellStyle = cellRightAlignmentStyle;
        }

        private void OnProductItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedProduct = productsListView.SelectedItem as Book;
            if (selectedProduct != null)
            {
                var suggestedProducts = _suggestionManager.GetRecommendedProducts(selectedProduct);
                new ProductInfoWindow(selectedProduct, _orderedProducts, suggestedProducts).ShowDialog();
                cartDataGrid.Items.Refresh();
            }
        }

        private void OpenCart_Click(object sender, RoutedEventArgs e)
        {
            TogglePages();
            InitDataGrid();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (!_orderedProducts.Any())
            {
                return;
            }
            
            Order order = new Order()
            {
                Date = DateTime.Now,
                OrderList = _orderedProducts.Select(x => new OrderLine()
                {
                    ProductUniqueId = x.UniqueId,
                    Quantity = x.Quantity,
                    Product = _allProducts.First(y => y.UniqueId == x.UniqueId)

                }).ToList()
            };
            _orderService.AddOrder(order);
            _orderedProducts.Clear();
            _allProducts = new ObservableCollection<Book>(_productService.GetAvailableProducts());
            productsListView.ItemsSource = _allProducts;
            TogglePages();
            var check = _documentService.CreatePrintDocument(order);
            new PrintWindow(check).ShowDialog();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            TogglePages();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            _orderedProducts.Clear();
        }

        private void TogglePages()
        {
            NewOrderPage.Visibility = NewOrderPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            CartPage.Visibility = CartPage.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = SearchTextBox.Text;
            var filteredBooks = _allProducts.Where(x => x.MainData.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            productsListView.ItemsSource = filteredBooks;
        }
    }
}