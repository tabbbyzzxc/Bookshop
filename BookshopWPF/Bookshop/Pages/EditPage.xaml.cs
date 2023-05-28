using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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
            var allProducts = _productService.GetAllProducts();
            InitializeComponent();
            listView.ItemsSource = _allProducts;

        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                var selectedBook = (Book)listView.SelectedItem;
                EditBookWindow editBookWindow = new EditBookWindow(selectedBook);
                editBookWindow.ShowDialog();
            }
        }
       
        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var text = Search.Text;
            var filteredBooks = _allProducts.Where(x => x.GetDescription().Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            listView.ItemsSource= filteredBooks;
        }
    }
}
