using Bookshop.ProductsLib;
using Bookshop.Services;
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

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for AddAudioBookPage.xaml
    /// </summary>
    public partial class AddAudioBookPage : Page
    {
        private AudioBookService audioBookRepo = new AudioBookService();
        public AddAudioBookPage()
        {
            InitializeComponent();
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

            var audioBook = new AudioBook(Description.Text, tempSell, tempBuy, tempTitle, tempAuth, GenreComboBox.Text, Language.Text, FormatComboBox.Text);


            if (audioBookRepo.AddAudioBook(audioBook))
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
    }
}
