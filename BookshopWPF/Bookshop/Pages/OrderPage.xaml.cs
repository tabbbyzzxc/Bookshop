using Bookshop.ProductsLib;
using Bookshop.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System;
using Bookshop.ViewModels;
using System.Linq;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private List<Product> _allProducts = new List<Product>();
        private ProductService _productService = new ProductService();
        private ObservableCollection<CartProductModel> _orderedItems = new ObservableCollection<CartProductModel>();
        public OrderPage()
        {
            InitializeComponent();
            _allProducts = _productService.GetAllProducts(true);
            listView.ItemsSource = _allProducts;
            orderDataGrid.ItemsSource = _orderedItems;
            orderDataGrid.CellEditEnding += OrderDataGrid_CellEditEnding;
        }

        private void OrderDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var inputText = ((TextBox)e.EditingElement).Text;
            var selected = orderDataGrid.SelectedItem as CartProductModel;
            if (selected != null && int.TryParse(inputText, out var quantity))
            {
                var product = _allProducts.First(x => x.Id == selected.Id);
                if (quantity > product.Quantity)
                {
                    selected.Quantity = product.Quantity;
                    ((TextBox)e.EditingElement).Text = product.Quantity.ToString();
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
            orderDataGrid.Columns[1].Width = 300;
            orderDataGrid.Columns[1].Header = "Product";
            orderDataGrid.Columns[1].IsReadOnly = true;
            orderDataGrid.Columns[2].Width = 100;
            orderDataGrid.Columns[2].Header = "Type";
            orderDataGrid.Columns[2].IsReadOnly = true;
            orderDataGrid.Columns[3].Width = 100;
            orderDataGrid.Columns[3].IsReadOnly = true;
            
            orderDataGrid.Columns[4].Width = 100; // quantity
            
            orderDataGrid.Columns[5].Width = 100;
            orderDataGrid.Columns[5].IsReadOnly = true;
        }

        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = listView.SelectedItem as Product;
            if (selectedItem != null)
            {

                new ProductInfoWindow(selectedItem, _orderedItems).ShowDialog();


            }
        }

        private void OpenCart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewOrderPage.Visibility = Visibility.Hidden;
            CartPage.Visibility = Visibility.Visible;
            InitDataGrid();
        }

        private void Order_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            NewOrderPage.Visibility = Visibility.Visible;
            CartPage.Visibility = Visibility.Hidden;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            _orderedItems.Clear();
        }
    }
}
