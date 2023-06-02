using Bookshop.ProductsLib;
using Bookshop.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for ProductInfoWindow.xaml
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        private Product _product;
        private readonly ObservableCollection<CartProductModel> _orderedItems;

        public ProductInfoWindow(Product product, ObservableCollection<CartProductModel> orderedItems, List<Product> suggestedProducts)
        {
            InitializeComponent();
           
            _product = product;
            _orderedItems = orderedItems;
            recommendedTextBlock.Text = string.Join(",\n ", suggestedProducts.Select(x => x.MainData));
            if (!CanOrderProduct())
            {
                addToCartBtn.IsEnabled = false;
                warningLabel.Visibility = Visibility.Visible;
            }
            RenderProductInfo();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var orderedProductExist = _orderedItems.FirstOrDefault(x => x.UniqueId == _product.UniqueId);
            if (orderedProductExist != null)
            {
                orderedProductExist.Quantity++;
            }
            else
            {
                var cartModel = new CartProductModel()
                {
                    ProductCode = _product.ProductCode,
                    UniqueId = _product.UniqueId,
                    ProductName = _product.MainData,
                    ProductType = _product.ProductType,
                    Price = _product.SellPrice,
                    Quantity = 1
                };

                _orderedItems.Add(cartModel);
            }

            Close();
        }

        private void RenderProductInfo()
        {
            informationLabel.Content = _product.ProductType;
            var fields = _product.GetProductInfoParameters();
            for (int i = 0; i < fields.Count; i++)
            {
                infoGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = GridLength.Auto
                });
                System.Collections.Generic.KeyValuePair<string, string> field = fields.ElementAt(i);
                var headerLabel = new Label
                {
                    Content = $"{field.Key}: ",
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetRow(headerLabel, i);

                var infoTextBlock = new TextBlock
                {
                    Text = field.Value,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                };

                Grid.SetRow(infoTextBlock, i);
                Grid.SetColumn(infoTextBlock, 1);
                infoGrid.Children.Add(infoTextBlock);

                infoGrid.Children.Add(headerLabel);
            }
        }

        private bool CanOrderProduct()
        {
            if (_product.Quantity == 0)
            {
                return false;
            }

            var orderedProductExist = _orderedItems.FirstOrDefault(x => x.UniqueId == _product.UniqueId);
            if (orderedProductExist == null)
            {
                return true;
            }

            return orderedProductExist.Quantity < _product.Quantity;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
