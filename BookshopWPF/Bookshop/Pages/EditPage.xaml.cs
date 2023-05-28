using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Bookshop.ProductsLib;
using Bookshop.ProductsLib.Repositories;

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
            var filteredBooks = _books.Where(x => x.Title.Contains(text, StringComparison.OrdinalIgnoreCase) || x.Author.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            listView.ItemsSource= filteredBooks;
        }
    }
}
