using System.Windows;
using System.Windows.Documents;

namespace Bookshop.Pages
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        public PrintWindow(FixedDocumentSequence document)
        {
            InitializeComponent();
            PreviewD.Document = document;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
