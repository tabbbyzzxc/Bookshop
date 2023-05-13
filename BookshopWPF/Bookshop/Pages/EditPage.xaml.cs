using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bookshop.Models;
using Bookshop.Repositories;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private List<Book> _books;
        public EditPage()
        {
            
            BookRepository repo = new BookRepository();
            _books= repo.GetAllBooks();
            InitializeComponent();
            listView.ItemsSource = _books;

        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                var selectedBook = (Book)listView.SelectedItem;
                EditBookWindow editBookWindow = new EditBookWindow(selectedBook);
                editBookWindow.ShowDialog();
            }
        }
       
        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var text = Search.Text;
            var filteredBooks = _books.Where(x => x.Name.Contains(text, StringComparison.OrdinalIgnoreCase) || x.Author.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            listView.ItemsSource= filteredBooks;
        }
    }
}
