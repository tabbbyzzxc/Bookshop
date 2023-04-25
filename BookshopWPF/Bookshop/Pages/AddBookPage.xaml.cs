using Bookshop.Models;
using Bookshop.Repositories;
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
                MessageBox.Show("The 'Name' field is required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string tempAuth = AuthorName.Text;
            decimal tempBuy, tempSell;
            bool ok = decimal.TryParse(BuyPrice.Text, out tempBuy);
            if (!ok)
            {
                MessageBox.Show("The 'Buy Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ok = decimal.TryParse(SellPrice.Text, out tempSell);
            if (!ok)
            {
                MessageBox.Show("The 'Sell Price' field must contain a numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var book = new Book(tempName, tempAuth, tempBuy, tempSell);
            var bookRepo = new BookRepository();
            
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

        private void AuthorName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
