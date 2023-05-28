using System.Windows;
using Bookshop.ProductsLib;
using Bookshop.ProductsLib.Repositories;

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
            BookRepository repo = new BookRepository();
            repo.UpdateBook(_book);
            Close();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
