﻿using Bookshop.ProductsLib;
using Bookshop.ViewModels;
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

        public ProductInfoWindow(Product product, ObservableCollection<CartProductModel> orderedItems, System.Collections.Generic.List<Product> suggestedProducts)
        {
            InitializeComponent();
           
            _product = product;
            _orderedItems = orderedItems;
            recommendedTextBlock.Text = string.Join(", ", suggestedProducts.Select(x => x.MainData));
            RenderInfo();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_product.Quantity == 0)
            {
                Close();
                return;
            }

            var orderProductExists = _orderedItems.FirstOrDefault(x => x.UniqueId == _product.UniqueId);

            if (orderProductExists != null)
            {
                orderProductExists.Quantity++;
            }
            else
            {

                var cartModel = new CartProductModel()
                {
                    Id = _product.Id,
                    UniqueId = _product.UniqueId,
                    ProductName = _product.MainData,
                    ProductType = _product.ProductType,
                    Price = _product.SellPrice,
                    Quantity = 1
                };

                _orderedItems.Add(cartModel);
                
            }

            _product.Quantity--;
            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RenderInfo()
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
    }
}