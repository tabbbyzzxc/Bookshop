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
using System.Windows.Shapes;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for EditAudioBookWindow.xaml
    /// </summary>
    public partial class EditAudioBookWindow : Window
    {
        public EditAudioBookWindow(AudioBook audioBook)
        {
            InitializeComponent();
            GenreComboBox.ItemsSource = AppConstants.Genres;
            FormatComboBox.ItemsSource = AppConstants.Formats;
            DataContext = audioBook;
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            AudioBookService audioBookService = new AudioBookService();
            audioBookService.UpdateAudioBook(DataContext as AudioBook);

            Close();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
