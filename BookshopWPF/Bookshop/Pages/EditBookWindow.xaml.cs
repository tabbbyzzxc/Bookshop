using System.Windows;
using Bookshop.ProductsLib;
using Bookshop.Services;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private Book _book;
        public EditBookWindow(Book book)
        {
            InitializeComponent();
            DataContext = book;
            _book = book;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            BookService repo = new BookService();
            repo.UpdateBook(_book);
            Close();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
