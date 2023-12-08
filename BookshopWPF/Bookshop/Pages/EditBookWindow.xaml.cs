using System.Windows;
using Bookshop.ProductsLib;
using Bookshop.Services;
using DataAccess;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public EditBookWindow(Book book)
        {
            InitializeComponent();
            GenreComboBox.ItemsSource = AppConstants.Genres;
            PaperTypeComboBox.ItemsSource = AppConstants.PaperTypes;
            DataContext = book;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            
            BookService bookService = new BookService();
            bookService.UpdateBook(DataContext as Book);
            
            Close();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
