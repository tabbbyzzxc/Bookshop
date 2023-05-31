using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bookshop.ProductsLib;
using Bookshop.Services;
using Bookshop.ViewModels;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for CheckoutPage.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {

        private ObservableCollection<Product> _availableProductList;
        private ObservableCollection<OrderLineViewModel> _orderLineList;
        private ProductService _productService = new ProductService();
        private OrderService _orderService = new OrderService();

        public CheckoutPage()
        {
            var allProduct = _productService.GetAllProducts(false);
            _availableProductList = new ObservableCollection<Product>(allProduct);
            _orderLineList = new ObservableCollection<OrderLineViewModel>();
            InitializeComponent();
            availableBooksListView.ItemsSource = _availableProductList;
            orderListView.ItemsSource = _orderLineList;
        }


        private void Button_Save(object sender, RoutedEventArgs e)
        {
            var order = new Order()
            {
                OrderList = _orderLineList.Select(x => new OrderLine
                {
                    Id = x.Id,
                    Quantity = x.Quantity

                }).ToList(),
                Date = DateTime.Now
            };
            if (_orderService.AddOrder(order))
            {
                MessageBox.Show("Order placed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderLineList.Clear();
                return;
            }
            else
            {
                MessageBox.Show("Order was empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _orderLineList.Clear();
            }
        }



        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            _orderLineList.Clear();
        }

        private void availableBooksListView_Add(object sender, MouseButtonEventArgs e)
        {
            Add();
            CalculateTotal();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Add();
            CalculateTotal();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            Remove();
            CalculateTotal();
        }
        private void orderListView_Remove(object sender, MouseButtonEventArgs e)
        {
            Remove();
            CalculateTotal();
        }
        private void Add()
        {
            var selectedItem = (BookAvailableModel)availableBooksListView.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Nothing selected. Select a book to Add", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_orderLineList.Any(x => x.Id == selectedItem.Id))
            {
                MessageBox.Show("Book already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var orderLine = new OrderLineViewModel()
            {
                Id = selectedItem.Id,
                Author = selectedItem.Author,
                Name = selectedItem.Name,
                SellPrice = selectedItem.SellPrice,
                Quantity = 1
            };

            _orderLineList.Add(orderLine);
        }

        private void Remove()
        {
            var selectedItem = (OrderLineViewModel)orderListView.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Nothing selected. Select a book to Remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _orderLineList.Remove(selectedItem);
        }

        private void CalculateTotal()
        {
            decimal sum = _orderLineList.Sum(x => x.Total);
            totalLabel.Content = sum.ToString();
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var text = Search.Text;
            var filteredBooks = _availableProductList.Where(x => x.GetDescription().Contains(text, StringComparison.OrdinalIgnoreCase)).ToList(); //переделать
            availableBooksListView.ItemsSource = filteredBooks;
        }
        
        private void OnQuantityChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (int.TryParse(textbox.Text, out var currentQuantity) && int.TryParse(textbox.Tag.ToString(), out var selectedId))
            {
                var selectedBook = _availableProductList.First(x => x.Id == selectedId);
                if (currentQuantity > selectedBook.Quantity)
                {
                    textbox.Text = selectedBook.Quantity.ToString();
                }
                
            }
            CalculateTotal();
        }
    }
}
