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
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tempName = NameBook.Text;
            if (String.IsNullOrWhiteSpace(tempName))
            {
                MessageBox.Show("Поле 'Назва' є обов'язковим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string tempAuth = AuthorName.Text;
            decimal tempGross, tempNet;
            bool ok = decimal.TryParse(GrossPrice.Text, out tempGross);
            if (!ok)
            {
                MessageBox.Show("Поле 'Ціна покупки' має містити числове значення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ok = decimal.TryParse(NetPrice.Text, out tempNet);
            if (!ok)
            {
                MessageBox.Show("Поле 'Ціна продажу' має містити числове значення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var book = new Book(tempName, tempAuth, tempGross, tempNet);
            var bookRepo = new BookRepository();
            
            if(bookRepo.AddBook(book))
            {
                MessageBox.Show("Книгу додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                AuthorName.Clear();
                NameBook.Clear();
                GrossPrice.Clear();
                NetPrice.Clear();
                return;
            }
            else
            {
                MessageBox.Show("Книга вже існує", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        private void AuthorName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
