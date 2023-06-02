using Bookshop.ProductsLib;
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
using System.Windows.Shapes;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for ProductAddQuantityWindow.xaml
    /// </summary>
    public partial class ProductAddQuantityWindow : Window
    {
        private Product _product;
        private ObservableCollection<CartProductModel> _cartModels;
        public ProductAddQuantityWindow(Product selectedItem, ObservableCollection<CartProductModel> cartModels)
        {
            InitializeComponent();
            _product = selectedItem;
            _cartModels = cartModels;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(quantityTextBox.Text, out var quantity))
            {
                Close();
                return;
            }

            var orderProductExists = _cartModels.FirstOrDefault(x => x.UniqueId == _product.UniqueId);

            if (orderProductExists != null)
            {
                orderProductExists.Quantity += quantity;
            }
            else
            {

                var cartModel = new CartProductModel()
                {
                    UniqueId = _product.UniqueId,
                    ProductName = _product.MainData,
                    ProductType = _product.ProductType,
                    Price = _product.SellPrice,
                    Quantity = quantity
                };

                _cartModels.Add(cartModel);
            }

            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
