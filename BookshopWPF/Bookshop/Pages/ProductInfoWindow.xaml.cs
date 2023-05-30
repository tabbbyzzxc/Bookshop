using Bookshop.ProductsLib;
using Bookshop.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for ProductInfoWindow.xaml
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        private readonly Product _product;
        private readonly ObservableCollection<CartProductModel> _orderedItems;

        public ProductInfoWindow(Product product, ObservableCollection<CartProductModel> orderedItems)
        {
            InitializeComponent();
            informationLabel.Content = product.GetDescription();
            _product = product;
            _orderedItems = orderedItems;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var cartModel = new CartProductModel()
            {
                Id = _product.Id,
                ProductName = _product.MainData,
                ProductType = _product.ProductType,
                Price = _product.SellPrice,
                Quantity = 1
            };
            _orderedItems.Add(cartModel);
            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
