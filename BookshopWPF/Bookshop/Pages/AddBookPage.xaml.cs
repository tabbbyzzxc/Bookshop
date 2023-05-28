using Bookshop.ProductsLib;
using Bookshop.ProductsLib.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        private BookRepository bookRepo = new BookRepository();
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tempName = NameBook.Text;
            if (string.IsNullOrWhiteSpace(tempName))
            {
                MessageBox.Show("The 'Name' field is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string tempAuth = AuthorName.Text;
            if (!decimal.TryParse(BuyPrice.Text, out var tempBuy))
            {
                MessageBox.Show("The 'Buy Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ;
            if (!decimal.TryParse(SellPrice.Text, out var tempSell))
            {
                MessageBox.Show("The 'Sell Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var book = new Book(tempName, tempAuth, tempBuy, tempSell);
           
            
            if(bookRepo.AddBook(book))
            {
                MessageBox.Show("The book has been added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                AuthorName.Clear();
                NameBook.Clear();
                BuyPrice.Clear();
                SellPrice.Clear();
                return;
            }
            else
            {
                MessageBox.Show("Book already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        
    }
}
