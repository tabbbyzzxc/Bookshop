﻿using System;
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
using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.ViewModels;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for CheckoutPage.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {

        private ObservableCollection<BookAvailableModel> _availableList;
        private ObservableCollection<OrderLineViewModel> _orderLineList;
        public CheckoutPage()
        {
            var repo = new BookQuantityRepository();
            var allBooks = repo.GetAvailableBooks();
            _availableList = new ObservableCollection<BookAvailableModel>(allBooks);
            _orderLineList = new ObservableCollection<OrderLineViewModel>();
            InitializeComponent();
            availableBooksListView.ItemsSource = _availableList;
            orderListView.ItemsSource = _orderLineList;
            CollectionViewSource.GetDefaultView(availableBooksListView.ItemsSource).Filter = Filter;


        }


        private void Button_Save(object sender, RoutedEventArgs e)
        {
            var repo = new OrderRepository();
            var order = new Order()
            {
                OrderList = _orderLineList.Select(x => new OrderLine
                {
                    Id = x.Id,
                    Author = x.Author,
                    Name = x.Name,
                    SellPrice = x.SellPrice,
                    Quantity = x.Quantity

                }).ToList(),
                Date = DateTime.Now
            };
            if (repo.PlaceOrder(order))
            {
                var repoQuantity = new BookQuantityRepository();
                repoQuantity.UpdateBookQuantity(order);
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
            var orderLine = new OrderLineViewModel()
            {
                Id = selectedItem.Id,
                Author = selectedItem.Author,
                Name = selectedItem.Name,
                SellPrice = selectedItem.SellPrice,
                Quantity = 1
            };
            foreach (var item in _orderLineList)
            {
                if (orderLine.Id == item.Id)
                {
                    MessageBox.Show("Book already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
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
            foreach (var item in _orderLineList)
            {
                if (item.Id == selectedItem.Id)
                {
                    _orderLineList.Remove(item);
                    return;
                }
            }
           
        }

        private void CalculateTotal()
        {
            decimal sum = 0;
            foreach (var book in _orderLineList)
            {
                sum += Convert.ToDecimal(book.Total);
            }
            totalLabel.Content = $"Total: {sum.ToString()}";
        }

   

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(availableBooksListView.ItemsSource).Refresh();
        }
        
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(Search.Text))
            {
                return true;
            }
            else
            {
                var data = item as BookAvailableModel;
                return (data.Author.IndexOf(Search.Text, StringComparison.OrdinalIgnoreCase) >= 0
                        || data.Name.IndexOf(Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        private void OnQuantityChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (int.TryParse(textbox.Text, out var currentQuantity) && int.TryParse(textbox.Tag.ToString(), out var selectedId))
            {
                foreach (var item in _availableList)
                {
                    if (item.Id == selectedId)
                    {
                        if (currentQuantity > item.Quantity)
                        {
                            textbox.Text = item.Quantity.ToString();
                        }
                    }
                }
            }
            CalculateTotal();
        }
    }
}