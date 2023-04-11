﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for ReceiptPage.xaml
    /// </summary>
    public partial class ReceiptPage : Page
    {
        private ObservableCollection<Book> _books;
        private ObservableCollection<BookIncomeModel> _bookIncomeModels;

        public ReceiptPage()
        {
            BookRepository repo = new BookRepository();
            var allBooks = repo.GetAllBooks();
            _books = new ObservableCollection<Book>(allBooks);
            _bookIncomeModels = new ObservableCollection<BookIncomeModel>();
            InitializeComponent();
            listView.ItemsSource = _books;
            arrivalListView.ItemsSource = _bookIncomeModels;
            
        }

        private void Add()
        {
            var selectedItem = (Book)listView.SelectedItem;
            if(selectedItem == null)
            {
                MessageBox.Show("Nothing selected. Select a book to Add", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var book = new BookIncomeModel
            {
                Id = selectedItem.Id,
                Name = selectedItem.Name,
                Author = selectedItem.Author,
                BuyPrice = selectedItem.BuyPrice
            };
            foreach (var item in _bookIncomeModels)
            {
                if (book.Id == item.Id)
                {
                    MessageBox.Show("Book already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            _bookIncomeModels.Add(book);
        }

        private void Remove()
        {
            var selectedItem = (BookIncomeModel)arrivalListView.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Nothing selected. Select a book to Remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var item in _bookIncomeModels)
            {
                if(item.Id == selectedItem.Id)
                {
                    _bookIncomeModels.Remove(item);
                    return;
                }
            }
        }

        private void listView_Add(object sender, MouseButtonEventArgs e)
        {
            Add();   
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            _bookIncomeModels.Clear();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            Remove();
        }

        private void arrivalListView_Remove(object sender, MouseButtonEventArgs e)
        {
            Remove();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            var repo = new BookQuantityRepository();
            var list = new List<BookQuantity>();
            
            foreach (var item in _bookIncomeModels)
            {
                list.Add(new BookQuantity(item.Id, item.Quantity));
            }
            repo.AddQuantity(list);
            MessageBox.Show("Book(s) added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _bookIncomeModels.Clear();
        }
    }
}
