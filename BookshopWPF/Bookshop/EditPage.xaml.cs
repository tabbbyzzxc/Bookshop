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

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public EditPage()
        {
            InitializeComponent();
            BookRepository repo = new BookRepository();
            listView.ItemsSource = repo.GetAllBooks();
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
    }
}
