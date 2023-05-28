using Bookshop.Services;
using System.Windows.Controls;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for MissingPage.xaml
    /// </summary>
    public partial class MissingPage : Page
    {
        public MissingPage()
        {
            var productService = new ProductService();
            
            InitializeComponent();
            listView.ItemsSource = productService.GetMissingProducts();
        }
    }
}
