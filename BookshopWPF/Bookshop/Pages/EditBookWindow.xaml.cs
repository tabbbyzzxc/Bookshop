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
using System.Windows.Shapes;
using Bookshop.Models;
using Bookshop.Repositories;

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
