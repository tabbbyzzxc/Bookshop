using Bookshop.ProductsLib;
using Bookshop.Services;
using System.Windows;
using System.Windows.Controls;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        private BookService bookRepo = new BookService();
        public AddBookPage()
        {
        
            InitializeComponent();
            GenreComboBox.ItemsSource = AppConstants.Genres;
            PaperTypeComboBox.ItemsSource = AppConstants.PaperTypes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            string tempTitle = NameBook.Text;
            if (string.IsNullOrWhiteSpace(tempTitle))
            {
                MessageBox.Show("The 'Title' field is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string tempAuth = AuthorName.Text;
            if (!decimal.TryParse(BuyPrice.Text, out var tempBuy))
            {
                MessageBox.Show("The 'Buy Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!decimal.TryParse(SellPrice.Text, out var tempSell))
            {
                MessageBox.Show("The 'Sell Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(PageCount.Text, out var tempPageCount))
            {
                MessageBox.Show("The 'Page Count' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var book = new Book(Description.Text, tempSell, tempBuy, tempTitle, tempAuth, GenreComboBox.Text, Language.Text, PaperTypeComboBox.Text, tempPageCount);
           
            
            if(bookRepo.AddBook(book))
            {
                MessageBox.Show("The book has been added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                AuthorName.Clear();
                NameBook.Clear();
                BuyPrice.Clear();
                SellPrice.Clear();
                GenreComboBox.SelectedIndex = 0;
                Language.Clear();
                PaperTypeComboBox.SelectedIndex = 0;
                PageCount.Clear();
                Description.Clear();
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
