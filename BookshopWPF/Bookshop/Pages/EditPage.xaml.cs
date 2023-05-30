using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Bookshop.Pages;
using Bookshop.ProductsLib;
using Bookshop.Services;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    { 
        private List<Product> _allProducts = new List<Product>();
        private ProductService _productService = new ProductService();
        public EditPage()
        {
            _allProducts = _productService.GetAllProducts(true);
            InitializeComponent();
            listView.ItemsSource = _allProducts;

        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                if(listView.SelectedItem is Book book)
                {
                    new EditBookWindow(book).ShowDialog();
                    
                }
                if(listView.SelectedItem is AudioBook audioBook)
                {
                    new EditAudioBookWindow(audioBook).ShowDialog();
                }
                _allProducts = _productService.GetAllProducts(true);
                listView.ItemsSource = _allProducts;
            }
        }
       
        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var text = Search.Text;
            var filteredBooks = _allProducts.Where(x => x.MainData.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            listView.ItemsSource= filteredBooks;
        }
    }
}
